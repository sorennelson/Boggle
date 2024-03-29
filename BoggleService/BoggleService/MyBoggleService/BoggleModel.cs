﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyBoggleService
{

    [DataContract]
    public class User
    {
        [DataMember(EmitDefaultValue = false)]
        public string Nickname { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UserToken { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? Score { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GameID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<PlayedWord> WordsPlayed { get; set; }

        
        public User() { }

        public User(Name name)
        {
            Nickname = name.Nickname;
        }

        public static User CreatedUser(string userToken)
        {
            User user = new User();
            user.UserToken = userToken;
            return user;
        }

        public User BriefUser()
        {
            User user = new User();
            user.Score = this.Score;
            return user;
        }

        public User ActiveLongUser()
        {
            User user = new User();
            user.Nickname = this.Nickname;
            user.Score = this.Score;

            return user;
        }

        public User CompletedLongUser()
        {
            User user = ActiveLongUser();
            if (this.WordsPlayed == null)
            {
                WordsPlayed = new List<PlayedWord>();
            }
            user.WordsPlayed = this.WordsPlayed;

            return user;
        }
    }

    [DataContract]
    public class Name
    {
        [DataMember]
        public string Nickname { get; set; }
    }

    [DataContract]
    public class PlayedWord
    {
        [DataMember]
        public string Word;

        [DataMember]
        public int Score;

        public PlayedWord(string word)
        {
            Word = word;
        }
    }



    [DataContract]
    public class Game
    {
        [DataMember]
        // Can be pending, active or completed
        public string GameState { get; set; }

        private DateTime StartTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int TimeLimit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GameID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public User Player1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public User Player2 { get; set; }

        public BoggleBoard BoggleBoard { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Board { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? TimeLeft { get; set; }

        public Game()
        {
            BoggleBoard = new BoggleBoard();
            Board = BoggleBoard.ToString();
        }

        public void SetStartTime(DateTime startTime)
        {
            if (startTime == null)
            {
                this.StartTime = DateTime.Now;
            }
            else
            {
                this.StartTime = startTime;
            }

        }

        public DateTime GetStartTime()
        {
            return StartTime;
        }

        static public Game PendingGame()
        {
            Game game = new Game();
            game.Board = null;
            game.GameState = "pending";
            return game;
        }

        public Game BriefGame()
        {
            Game game = new Game();

            int left = (int)(StartTime.AddSeconds((double)TimeLimit) - DateTime.Now).TotalSeconds;
            if (GameState == "completed" || left <= 0)
            {
                TimeLeft = 0;
                GameState = "completed";
            }
            else
            {
                TimeLeft = left;
            }

            game.Board = null;
            game.TimeLeft = TimeLeft;
            game.GameState = GameState;
            game.Player1 = Player1.BriefUser();
            game.Player2 = Player2.BriefUser();
            return game;
        }

        public Game StatusLong()
        {
            Game game = new Game();
            game.Board = BoggleBoard.ToString();
            game.TimeLimit = TimeLimit;

            game.Player1 = Player1.ActiveLongUser();
            game.Player2 = Player2.ActiveLongUser();

            int left = (int)(StartTime.AddSeconds((double)TimeLimit) - DateTime.Now).TotalSeconds;
            if (GameState == "completed" || left <= 0)
            {
                TimeLeft = 0;
                GameState = "completed";
                game.Player1 = Player1.CompletedLongUser();
                game.Player2 = Player2.CompletedLongUser();
            }
            else
            {
                TimeLeft = left;
            }

            game.TimeLeft = TimeLeft;
            game.GameState = GameState;
            return game;
        }

    }

    [DataContract]
    public class SetGame
    {
        [DataMember(EmitDefaultValue = false)]
        public string UserToken { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? TimeLimit { get; set; }
 
        [DataMember]
        public string GameID { get; set; }
        
        public SetGame(string GameID)
        {
            UserToken = null;
            TimeLimit = null;
            this.GameID = GameID;
        }
        
    }

    public class CancelRequestDetails
    {
        public string UserToken { get; set; }
    }

    [DataContract]
    public class PlayWordDetails
    {
        [DataMember(EmitDefaultValue = false)]
        public string UserToken { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Word { get; set; }

        [DataMember]
        public int Score { get; set; }

        public PlayWordDetails(int score)
        {
            Score = score;
        }
    }

}