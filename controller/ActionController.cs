using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperShortcut.controller
{
    class ActionController
    {

        private Array PreviousActions = new string[,] {  };

        // Runs a program
        public bool ExecuteProgram(string path)
        {

            path = path.Replace("\"", "").Trim();

            if(File.Exists(path) || Directory.Exists(path))
            {
                
                if (!path.Contains(".exe") && !path.Contains(".dll") && !path.Contains(".jar")) return false;
                Process.Start(path);

                string[] pathParts = misc.Utilities.StrExplode('\\', path);
                misc.Utilities.WriteColoredLine("Program executed: " + pathParts[pathParts.Length - 1], ConsoleColor.Green);

                return true;

            }
            else 
            {
             
                string[] pathParts = misc.Utilities.StrExplode('\\', path);
                misc.Utilities.WriteColoredLine("Failed to execute program " + pathParts[pathParts.Length - 1], ConsoleColor.Red);
                return false;

            }

        } // public bool ExecuteProgram()

        // Executes a routine
        public bool ExecuteRoutine(Routine routine)
        {

            string Errors = "";

            // Expression

            return String.IsNullOrEmpty(Errors) ? true : false;

        } // public bool ExecuteRoutine()

        // Structs a new Routine
        public static Routine AddRoutine(string routineName)
        {

            return new Routine(routineName);

        } // public static Routine AddRoutine()

    }
}
