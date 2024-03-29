﻿namespace PS8
{
    partial class BoggleGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.player1Name = new System.Windows.Forms.Label();
            this.player2Name = new System.Windows.Forms.Label();
            this.player1Score = new System.Windows.Forms.Label();
            this.player2Score = new System.Windows.Forms.Label();
            this.wordTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.timeLeft = new System.Windows.Forms.Label();
            this.EnterButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.player1WordList = new System.Windows.Forms.ListBox();
            this.player2WordList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // player1Name
            // 
            this.player1Name.AutoSize = true;
            this.player1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1Name.Location = new System.Drawing.Point(30, 33);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(80, 13);
            this.player1Name.TabIndex = 0;
            this.player1Name.Text = "player1Name";
            // 
            // player2Name
            // 
            this.player2Name.AutoSize = true;
            this.player2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2Name.Location = new System.Drawing.Point(366, 33);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(80, 13);
            this.player2Name.TabIndex = 1;
            this.player2Name.Text = "player2Name";
            // 
            // player1Score
            // 
            this.player1Score.AutoSize = true;
            this.player1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1Score.Location = new System.Drawing.Point(30, 67);
            this.player1Score.Name = "player1Score";
            this.player1Score.Size = new System.Drawing.Size(81, 13);
            this.player1Score.TabIndex = 2;
            this.player1Score.Text = "player1Score";
            // 
            // player2Score
            // 
            this.player2Score.AutoSize = true;
            this.player2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2Score.Location = new System.Drawing.Point(366, 67);
            this.player2Score.Name = "player2Score";
            this.player2Score.Size = new System.Drawing.Size(81, 13);
            this.player2Score.TabIndex = 3;
            this.player2Score.Text = "player2Score";
            // 
            // wordTextBox
            // 
            this.wordTextBox.Location = new System.Drawing.Point(179, 299);
            this.wordTextBox.Name = "wordTextBox";
            this.wordTextBox.Size = new System.Drawing.Size(108, 20);
            this.wordTextBox.TabIndex = 4;
            this.wordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wordTextBox_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label16, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label15, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 2);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(160, 112);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(155, 145);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(117, 108);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 37);
            this.label16.TabIndex = 48;
            this.label16.Text = "label16";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(79, 108);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 37);
            this.label15.TabIndex = 47;
            this.label15.Text = "label15";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(41, 108);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 37);
            this.label14.TabIndex = 46;
            this.label14.Text = "label14";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 108);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 37);
            this.label13.TabIndex = 45;
            this.label13.Text = "label13";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(79, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 36);
            this.label11.TabIndex = 43;
            this.label11.Text = "label11";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 36);
            this.label10.TabIndex = 42;
            this.label10.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 36);
            this.label9.TabIndex = 41;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(117, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 36);
            this.label8.TabIndex = 40;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 36);
            this.label7.TabIndex = 39;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 36);
            this.label6.TabIndex = 38;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 36);
            this.label5.TabIndex = 37;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 36);
            this.label4.TabIndex = 36;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 36);
            this.label3.TabIndex = 35;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 36);
            this.label2.TabIndex = 34;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 36);
            this.label1.TabIndex = 33;
            this.label1.Text = "label1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(117, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 36);
            this.label12.TabIndex = 44;
            this.label12.Text = "label12";
            // 
            // timeLeft
            // 
            this.timeLeft.AutoSize = true;
            this.timeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLeft.Location = new System.Drawing.Point(225, 33);
            this.timeLeft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeLeft.Name = "timeLeft";
            this.timeLeft.Size = new System.Drawing.Size(21, 13);
            this.timeLeft.TabIndex = 33;
            this.timeLeft.Text = "00";
            // 
            // EnterButton
            // 
            this.EnterButton.Location = new System.Drawing.Point(376, 296);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(2);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(54, 23);
            this.EnterButton.TabIndex = 34;
            this.EnterButton.Text = "Enter";
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.Enter_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(479, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // player1WordList
            // 
            this.player1WordList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.player1WordList.FormattingEnabled = true;
            this.player1WordList.Location = new System.Drawing.Point(12, 112);
            this.player1WordList.Name = "player1WordList";
            this.player1WordList.Size = new System.Drawing.Size(120, 147);
            this.player1WordList.TabIndex = 36;
            // 
            // player2WordList
            // 
            this.player2WordList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.player2WordList.FormattingEnabled = true;
            this.player2WordList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.player2WordList.Location = new System.Drawing.Point(347, 112);
            this.player2WordList.Name = "player2WordList";
            this.player2WordList.Size = new System.Drawing.Size(120, 147);
            this.player2WordList.TabIndex = 37;
            // 
            // BoggleGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 346);
            this.Controls.Add(this.player2WordList);
            this.Controls.Add(this.player1WordList);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.timeLeft);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.wordTextBox);
            this.Controls.Add(this.player2Score);
            this.Controls.Add(this.player1Score);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player1Name);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(495, 385);
            this.MinimumSize = new System.Drawing.Size(495, 385);
            this.Name = "BoggleGame";
            this.Text = "Boggle Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoggleGame_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label player1Name;
        private System.Windows.Forms.Label player2Name;
        private System.Windows.Forms.Label player1Score;
        private System.Windows.Forms.Label player2Score;
        private System.Windows.Forms.TextBox wordTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label timeLeft;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListBox player1WordList;
        private System.Windows.Forms.ListBox player2WordList;
    }
}