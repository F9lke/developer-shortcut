using System;
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

            if (!path.Contains(".exe")) return false;

            Process.Start(path);

            return true;

        } // public void ExecuteProgram()

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
