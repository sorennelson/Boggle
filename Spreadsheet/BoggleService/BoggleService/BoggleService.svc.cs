﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        // Connection string for the Database
        private static string BoggleDB;
        public BoggleService()
        {
            // Fetches connection string web.config
            BoggleDB = ConfigurationManager.ConnectionStrings["BoggleDB"].ConnectionString;
        }

        private readonly static Dictionary<String, User> users = new Dictionary<string, User>();
        // Games only contain active and completed
        private readonly static Dictionary<String, Game> games = new Dictionary<string, Game>();
        private readonly static HashSet<Game> pendingGames = new HashSet<Game>();
        private readonly static HashSet<User> pendingUsers = new HashSet<User>();
        private readonly static HashSet<int> pendingTimeLimits = new HashSet<int>();
        private static readonly object sync = new object();

        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }

        /// <summary>
        /// Returns true or false depending on if the nickname is valid
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        private bool IsNicknameValid(string nickname)
        {
            if (nickname == null || nickname.Trim().Length == 0 || nickname.Trim().Length > 50)
            {
                return false;
            }
            return true;
        }

        public string CreateUser(User user)
        {
            if (!IsNicknameValid(user.Nickname))
            {
                SetStatus(Forbidden);
                return null;
            }

            // Connection to database
            using (SqlConnection connection = new SqlConnection(BoggleDB))
            {
                connection.Open();

                // Transaction for databse commands
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    // SQL command to run
                    using (SqlCommand command = new SqlCommand(
                        "insert into Users (UserID, Nickname) values(@UserID, @Nickname)",
                        connection,
                        transaction))
                    {
                        string userID = Guid.NewGuid().ToString();

                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Nickname", user.Nickname);

                        if (command.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("Query failed unexpectedly");
                        }

                        SetStatus(Created);

                        // To avoid rollback after control has left the scope
                        transaction.Commit();
                        return userID;
                    }
                }
            }
        }

        public string JoinGame(SetGame setGame)
        {

            if (setGame.UserToken == null)
            {
                SetStatus(Forbidden);
                return null;
            }

            if (setGame.TimeLimit < 5 || setGame.TimeLimit > 120)
            {
                SetStatus(Forbidden);
                return null;
            }

            using (SqlConnection connection = new SqlConnection(BoggleDB))
            {
                connection.Open();

                // Transaction for databse commands
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    // SQL command to run
                    // To check if user ID is valid
                    using (SqlCommand command = new SqlCommand(
                        "select UserID from Users where UserID = @UserID",
                        connection,
                        transaction))
                    {
                        command.Parameters.AddWithValue("@UserID", setGame.UserToken);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // User ID is invalid
                            if (!reader.HasRows)
                            {
                                SetStatus(Forbidden);
                                reader.Close();
                                transaction.Commit();
                                return null;
                            }
                        }
                    }

                    // SQL command to run
                    // To check if user is in game
                    using (SqlCommand command = new SqlCommand(
                        "select * from Games where Games.Player1 = @UserID or Games.Player2 = @UserID",
                        connection,
                        transaction))
                    {
                        command.Parameters.AddWithValue("@UserID", setGame.UserToken);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                SetStatus(Conflict);
                                reader.Close();
                                transaction.Commit();
                                return null;
                            }
                        }
                    }

                    // If there is a user waiting with the same time limit, add current user to game
                    using (SqlCommand command = new SqlCommand(
                        "select * from Games where Games.Player2 is NULL",
                        connection,
                        transaction))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // No pending games to join, make a new pending game
                            if (!reader.HasRows)
                            {
                                using (SqlCommand addCommand = new SqlCommand(
                                    "insert into Games(Player1) output inserted.GameID  values (@Player1)",
                                    connection,
                                    transaction))
                                {
                                    addCommand.Parameters.AddWithValue("@Player1", setGame.UserToken);

                                    if (addCommand.ExecuteNonQuery() != 1)
                                    {
                                        throw new Exception("Query failed unexpectedly");
                                    }

                                    string gameID = addCommand.ExecuteScalar().ToString();
                                    SetStatus(Accepted);
                                    reader.Close();
                                    transaction.Commit();
                                    return gameID;
                                }
                            }
                            else
                            {

                                // reader consists of gameID and player1
                                // need to set Player2, Board, TimeLimit, StartTime
                                using (SqlCommand joinGameCommand = new SqlCommand(
                                    "update Games set Player2 = @Player2, Board = @Board, TimeLimit = @TimeLimit," +
                                    " StartTime = @StartTime where GameID = @GameID",
                                    connection,
                                    transaction))
                                {
                                    Game game = new Game();
                                    joinGameCommand.Parameters.AddWithValue("@Player2", setGame.UserToken);
                                    joinGameCommand.Parameters.AddWithValue("@Board", game.Board);
                                    joinGameCommand.Parameters.AddWithValue("@TimeLimit", setGame.TimeLimit);
                                    joinGameCommand.Parameters.AddWithValue("@StartTime", DateTime.Now);
                                    reader.Read();
                                    string id = reader["GameID"].ToString();
                                    joinGameCommand.Parameters.AddWithValue("@GameID", id);

                                    reader.Close();
                                    if (joinGameCommand.ExecuteNonQuery() == 0)
                                    {
                                        SetStatus(Forbidden);
                                    }
                                    else
                                    {
                                        SetStatus(Created);
                                    }
                                    transaction.Commit();
                                    return id;
                                }
                            }
                        }
                    }
                }
            }
        }

        // TODO: TEST & Add GameState to Game
        public void CancelJoinRequest(CancelRequestDetails cancelRequestDetails)
        {
            lock (sync)
            {
                using (SqlConnection connection = new SqlConnection(BoggleDB))
                {
                    connection.Open();

                    // Transaction for databse commands
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = new SqlCommand(
                            "select * from Games where Player1.UserID = @UserID and Player2.UserID = null",
                            connection,
                            transaction))
                        {
                            command.Parameters.AddWithValue("@UserID", cancelRequestDetails.UserToken);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    // No pending game
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    transaction.Commit();
                                    return;
                                }
                            }
                        }

                        using (SqlCommand command = new SqlCommand(
                           "delete from Games where Player1.UserID = @UserID",
                           connection,
                           transaction))
                        {
                            command.Parameters.AddWithValue("@UserID", cancelRequestDetails.UserToken);

                            if (command.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("Query failed unexpectedly");
                            }
                        }

                        SetStatus(OK);
                        transaction.Commit();
                    }
                }
            }
        }

        public int? PlayWord(string GameID, PlayWordDetails PlayWordDetails)
        {
            string word = PlayWordDetails.Word.Trim();
            PlayedWord playedWord = new PlayedWord(word);

            if (word == "" || word == null || word.Length > 30)
            {
                SetStatus(Forbidden);
                return null;
            }


            using (SqlConnection connection = new SqlConnection(BoggleDB))
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    // Check if game exists
                    using (SqlCommand command = new SqlCommand(
                            "select * from Games where Games.GameID = @GameID",
                            connection,
                            transaction))
                    {
                        command.Parameters.AddWithValue("@GameID", GameID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                SetStatus(Forbidden);
                                reader.Close();
                                transaction.Commit();
                                return null;
                            }

                            // Game exists, check if it is active
                            reader.Read();
                            if (reader["Player2"] == null || ((DateTime)reader["StartTime"] - DateTime.Now).TotalSeconds <= 0)
                            {
                                SetStatus(Conflict);
                                reader.Close();
                                transaction.Commit();
                                return null;
                            }
                        }
                    }

                    // Check if user exists in game
                    using (SqlCommand command = new SqlCommand(
                            "select * from Games where GameID = @GameID",
                            connection,
                            transaction))
                    {
                        command.Parameters.AddWithValue("@GameID", GameID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();

                            // User is in this game
                            if (reader["Player1"].ToString() == PlayWordDetails.UserToken ||
                                reader["Player2"].ToString() == PlayWordDetails.UserToken)
                            {
                                BoggleBoard board = new BoggleBoard(reader["Board"].ToString());
                                reader.Close();

                                List<string> words = new List<string>();

                                // Get all words
                                using (SqlCommand wordCommand = new SqlCommand(
                                    "select Word from Words where Player = @Player",
                                    connection,
                                    transaction))
                                {
                                    wordCommand.Parameters.AddWithValue("@Player", PlayWordDetails.UserToken);

                                    // Get list of words played
                                    using (SqlDataReader wordReader = wordCommand.ExecuteReader())
                                    {
                                        while (wordReader.Read())
                                        {
                                            words.Add(reader["Word"].ToString());
                                        }
                                        wordReader.Close();
                                    }

                                    int score = GetWordScore(PlayWordDetails.Word, board, words);

                                    // Add word to word table
                                    using (SqlCommand addWordCommand = new SqlCommand(
                                        "insert into Words (Word, GameID, Player, Score) " +
                                        "values(@Word, @GameID, @Player, @Score)",
                                        connection,
                                        transaction))
                                    {
                                        addWordCommand.Parameters.AddWithValue("@Word", PlayWordDetails.Word);
                                        addWordCommand.Parameters.AddWithValue("@GameID", GameID);
                                        addWordCommand.Parameters.AddWithValue("@Player", PlayWordDetails.UserToken);
                                        addWordCommand.Parameters.AddWithValue("@Score", score);

                                        if (command.ExecuteNonQuery() != 1)
                                        {
                                            throw new Exception("Query failed unexpectedly");
                                        }
                                        SetStatus(OK);
                                        transaction.Commit();
                                        return score;
                                    }
                                }
                            }

                            // User is not in this game
                            else
                            {
                                SetStatus(Forbidden);
                                reader.Close();
                                transaction.Commit();
                                return null;
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Returns score of given word whilist accounting for words played
        /// </summary>
        /// <param name="word"></param>
        /// <param name="board"></param>
        /// <param name="playerWordList"></param>
        /// <returns></returns>
        private int GetWordScore(string word, BoggleBoard board, List<string> playerWordList)
        {
            PlayedWord playedWord = new PlayedWord(word);
            if (word.Length < 3)
            {
                return 0;
            }

            // Check if word can be formed
            if (!board.CanBeFormed(word))
            {
                return -1;
            }

            // Check if word is legal
            string contents = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/dictionary.txt");

            if (contents.Contains(word))
            {
                if (playerWordList.Contains(playedWord.Word))
                {
                    return 0;
                }
                switch (word.Length)
                {
                    case 3:
                    case 4:
                        return 1;
                    case 5:
                        return 2;
                    case 6:
                        return 3;
                    case 7:
                        return 5;
                    // In this case, default is being used for if word length > 7
                    // Must be a better way to do this
                    default:
                        return 11;
                }
            }

            return -1;

        }


        // TODO: Test
        public Game GameStatus(string gameID, string brief)
        {

            lock (sync)
            {
                using (SqlConnection connection = new SqlConnection(BoggleDB))
                {
                    connection.Open();

                    // Transaction for databse commands
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = new SqlCommand(
                            "select * from Games where GameID = @GameID",
                            connection,
                            transaction))
                        {
                            command.Parameters.AddWithValue("@GameID", gameID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    // No game with that ID
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    transaction.Commit();
                                    return null;
                                }

                                while (reader.Read())
                                {
                                    // pending
                                    if (reader["Player2"].ToString() == "")
                                    {
                                        Game pendingGame = Game.PendingGame();
                                        SetStatus(OK);
                                        reader.Close();
                                        transaction.Commit();
                                        return pendingGame;
                                    }

                                    Game game = new Game();
                                    game.TimeLimit = (int)reader["TimeLimit"];
                                    game.SetStartTime((DateTime)reader["StartTime"]);

                                    bool isCompleted = false;
                                    game.GameState = "active";
                                    int left = (int)(game.GetStartTime().AddSeconds((double)game.TimeLimit) - DateTime.Now).TotalSeconds;
                                    if (left <= 0)
                                    {
                                        isCompleted = true;
                                        game.GameState = "completed";
                                    }

                                    string player1ID = (string)reader["Player1"];
                                    string player2ID = (string)reader["Player2"];
                                    User player1 = GetPlayer(player1ID, connection, transaction, brief, isCompleted);
                                    User player2 = GetPlayer(player2ID, connection, transaction, brief, isCompleted);
                                    game.Player1 = player1;
                                    game.Player2 = player2;

                                    if (brief == "yes")
                                    {
                                        SetStatus(OK);
                                        reader.Close();
                                        transaction.Commit();
                                        return game.BriefGame();
                                    }

                                    game.Board = (string)reader["Board"];
                                    SetStatus(OK);
                                    reader.Close();
                                    transaction.Commit();
                                    return game.StatusLong();
                                }
                            }
                        }
                    }
                }
            }

            SetStatus(Forbidden);
            return null;
        }

        /// <summary>
        /// Returns the User from the DataBase
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="brief">If "yes" then only score is set</param>
        /// <param name="isCompleted">If false then only score and nickname is set otherwise score, nickname, and words are set</param>
        /// <returns></returns>
        private User GetPlayer(string userID, SqlConnection connection, SqlTransaction transaction, string brief, Boolean isCompleted)
        {
            User player = new User();
            using (SqlCommand command = new SqlCommand(
                            "select * from Users where UserID = @UserID",
                            connection, transaction))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new Exception("Query failed unexpectedly");
                    }

                    player.Score = 0;
                    List<PlayedWord> words = GetWords(userID, connection, transaction);
                    if (words.Count() > 0)
                    {
                        foreach (PlayedWord word in words)
                        {
                            player.Score += word.Score;
                        }
                        if (isCompleted)
                        {
                            player.WordsPlayed = words;
                        }
                    }

                    while (reader.Read())
                    {
                        if (reader["Nickname"] == null)
                        {
                            string s = "null";
                        }
                        if (brief != "yes")
                        {
                            string s = (string)reader["Nickname"];
                            player.Nickname = reader["Nickname"].ToString();
                        }
                    }
                }
            }

            return player;
        }

        /// <summary>
        /// Returns the Played words for the UserID Given
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private List<PlayedWord> GetWords(string userID, SqlConnection connection, SqlTransaction transaction)
        {
            List<PlayedWord> words = new List<PlayedWord>();
            using (SqlCommand command = new SqlCommand(
                            "select * from Words where Player = @UserID",
                            connection, transaction))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PlayedWord word = new PlayedWord(reader["Word"].ToString());
                        word.Score = (int)reader["Score"];
                        words.Add(word);
                    }
                }
            }

            return words;
        }


    }

}
