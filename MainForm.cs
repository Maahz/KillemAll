using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillemAll
{
    public partial class MainForm : Form
    {

        public static string _historyFile = "history.log";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Start window in lower left corner
            Location = new Point(0, SystemInformation.VirtualScreen.Height - this.Height);

            //Accept Enter and Esc as OK and Cancel button presses
            AcceptButton = okBtn;
            CancelButton = cancelBtn;

            okBtn.Enabled = false;  //Default OK button state

            //Load history to commandBox
            LoadHistory();

            //Give Focus
            if (!this.ContainsFocus)
            {
                this.Focus();
            }
        }

        /*
         * Buttons are here
         */
        #region "Buttons"
        private void okBtn_Click(object sender, EventArgs e)
        {
            string command = commandBox.Text;

            //Check that command is valid, ie. explorer.exe
            if (!ValidCommand(command) || !command.Contains(".exe"))
            {
                ClearCommand();
                throw new InvalidCommandException("Command formating error.");
            }
            
            if (!ProcessExists(command))  //Show error if process didn't exist
            {
                
                ClearCommand();
                throw new InvalidCommandException("Process " + command + " not found!");
            }
            try
            {
                ProcessHandler.KillProcess(command);
                Application.Exit();
            }
            catch (Exception)
            {

                throw;
            }
            
            ClearCommand();
        }

       

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            ShowProcessThree();
        }
        #endregion

        //Simple command to Clear text box and save history
        private void ClearCommand()
        {
            SaveHistory(commandBox.Text);
            commandBox.Text = "";
        }

        /// <summary>
        /// Save history
        /// </summary>
        /// <param name="history"></param>
        private void SaveHistory(string history)
        {
            StreamWriter writer = new StreamWriter(_historyFile,true);
            //If file doesn't exist, create it
            if (!File.Exists(_historyFile))
            {
                File.Create(_historyFile);
            }
            commandBox.Items.Add(history);
            try
            {
                writer.Write(commandBox.Text + "\n");
            }
            catch (Exception)
            {
                writer.Close();
            }
            writer.Close();
            
        }

        /// <summary>
        /// Load History from _historyFile
        /// </summary>
        private void LoadHistory()
        {
            //Variables
            StreamReader reader;
            string line = "";
            int counter = 0;

            try
            {
                reader = new StreamReader(_historyFile);
            }
            catch (Exception)
            {
                return;
            }
            
            while ((line = reader.ReadLine()) != null)
            {
                commandBox.Items.Add(line);
                counter++;
            }
            reader.Close();
        }
        /// <summary>
        /// Show all available processes.
        /// </summary>
        private void ShowProcessThree()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check that command is valid.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool ValidCommand(string text)
        {
            string[] command = new string[2];
            try
            {
                command = text.Split('.');
            }
            catch (Exception)
            {
                throw new InvalidCommandException("String not formated properly.");
            }
            
            if (command.Length > 2 )  //If command is too long or weird...return false
            {
                throw new InvalidCommandException("Command not valid.");
            }
            return true;
        }

        /// <summary>
        /// Check if process exists
        /// </summary>
        /// <param name="name">Name of process</param>
        /// <returns>Bool</returns>
        private bool ProcessExists(string name)  //Check if process exists
        {
            string[] splitName = name.Split('.');
            Process[] process = Process.GetProcessesByName(splitName[0]);
            if (process.Length > 0)
            {
                return true;
            }
            else
            {
                throw new ProcessNotFoundException("Process not found");
            }
        }


        /// <summary>
        /// Enable OK button if box has text
        /// </summary>
        private void commandBox_TextChanged(object sender, EventArgs e)  
        {
            if (commandBox.Text != "")
            {
                okBtn.Enabled = true;
            }
            else
            {
                okBtn.Enabled = false;
            }
            
        }

        /// <summary>
        /// Key event handler. CURRENTLY NOT IN USE
        /// Shortcut handled by program "Shortcutter"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
