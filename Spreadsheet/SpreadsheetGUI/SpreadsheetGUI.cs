﻿using SS;
using SSGui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class SpreadsheetGUI : Form, ISpreadsheetView
    {

        /// <summary>
        /// Offsets for resizing so the SpreadsheetPanel does not get hidden under the GUI.
        /// </summary>
        private int panelWidthOffset, panelHeightOffset;

        /// <summary>
        /// Fired when request is made to set content.
        /// The parameter is the content to be set.
        /// </summary>
        public event Action<int, int, string> SetContentEvent;
        public event Action<int, int, TextBox, TextBox> SelectionChangeEvent;
        /// <summary>
        /// Fired when request is made to close window
        /// </summary>
        public event Action CloseEvent;
        public event Action<FileStream> SaveEvent;
        public event Action NewEvent;
        public event Action HelpEvent;

        // fired when checking whether a save is necessary
        public event Action DidChangeEvent;

        public SpreadsheetGUI()
        {
            InitializeComponent();
            panelWidthOffset = this.Width - spreadsheetPanel1.Width;
            panelHeightOffset = this.Height - spreadsheetPanel1.Height;
            KeyPreview = true;
            spreadsheetPanel1.SelectionChanged += displaySelection;
        }

        /// <summary>
        /// Updates value and content text boxes to show correct information.
        /// </summary>
        /// <param name="ss"></param>
        private void displaySelection(SpreadsheetPanel ss)
        {
            if (SelectionChangeEvent != null)
            {
                int column, row;
                ss.GetSelection(out column, out row);
                ss.SetSelection(column, row);
                SelectionChangeEvent(column, row, cellValueTextBox, cellContentTextBox);
            }
        }


        /// <summary>
        /// Closes SpreadsheetGUI
        /// </summary>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseEvent?.Invoke();
        }

        /// <summary>
        /// Allows to naviagte cells using arrow keys.
        /// </summary>
        private void SpreadsheetGUI_KeyDown(object sender, KeyEventArgs e)
        {
            int column, row;
            spreadsheetPanel1.GetSelection(out column, out row);
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (row == 98)
                    {
                        break;
                    }
                    spreadsheetPanel1.SetSelection(column, row + 1);
                    // displaySelection here may not be necessary
                    displaySelection(spreadsheetPanel1);
                    e.Handled = true;
                    break;
                case Keys.Up:
                    if (row == 0)
                    {
                        break;
                    }
                    spreadsheetPanel1.SetSelection(column, row - 1);
                    // displaySelection here may not be necessary
                    displaySelection(spreadsheetPanel1);
                    e.Handled = true;
                    break;
                case Keys.Left:
                    if (column == 0)
                    {
                        break;
                    }
                    spreadsheetPanel1.SetSelection(column - 1, row);
                    // displaySelection here may not be necessary
                    displaySelection(spreadsheetPanel1);
                    e.Handled = true;
                    break;
                case Keys.Right:
                    if (column == 26)
                    {
                        break;
                    }
                    spreadsheetPanel1.SetSelection(column + 1, row);
                    // displaySelection here may not be necessary
                    displaySelection(spreadsheetPanel1);
                    e.Handled = true;
                    break;
            }
        }


        /// <summary>
        /// Once enter is pressed while content text box is in focus,
        /// sets content value for selected cell to the text that is
        /// in the text box.
        /// </summary>
        private void cellContentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                int column, row;
                spreadsheetPanel1.GetSelection(out column, out row);
                if (SetContentEvent != null)
                {
                    SetContentEvent(column, row, cellContentTextBox.Text);
                }
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Resizes SpreadsheetPanel based on SpreadsheetGUI.
        /// </summary>
        private void SpreadsheetGUI_Resize(object sender, EventArgs e)
        {
            spreadsheetPanel1.Width = this.Width - panelWidthOffset;
            spreadsheetPanel1.Height = this.Height - panelHeightOffset;

        }

        public void OpenNew()
        {
            // TODO:
            throw new NotImplementedException();
        }


        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpEvent?.Invoke();
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        public void CloseWindow()
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) => NewEvent?.Invoke();

        public void SetCellValue(int column, int row, string content)
        {
            displaySelection(spreadsheetPanel1);
            spreadsheetPanel1.SetValue(column, row, content);
        } 

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e) => DidChangeEvent?.Invoke();


        public void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Spreadsheet";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                SaveEvent?.Invoke(fs);
            }

        }
    }
}