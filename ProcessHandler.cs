using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillemAll
{
    class ProcessHandler
    {
        /// <summary>
        /// List all processes on the computer
        /// </summary>
        /// <returns>List<string> Process names</string></returns>
        public static List<string> ListProcesses()
        {
            List<string> procNames = new List<string>();
            List<Process> processes = Process.GetProcesses().ToList();
            foreach (Process process in processes)
            {
                if (!procNames.Contains(process.ProcessName))  //Check if process is already listed
                {
                    procNames.Add(process.ProcessName);
                }
                
            }
            if (procNames.Count > 0)
            {
                return procNames;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Kill process
        /// </summary>
        /// <returns>true if success, else false</returns>
        public static bool KillProcess(string processName)
        {
            string[] splitName = processName.Split('.');
            Process[] processes = Process.GetProcessesByName(splitName[0]);
            foreach (Process process in processes)
            {
                process.Kill();
                System.Windows.Forms.MessageBox.Show("Process " + process.ProcessName + " terminated.");
            }

            return true;
        }

    }
}
