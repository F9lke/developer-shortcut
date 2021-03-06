﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DeveloperShortcut.controller
{
    class ActionController
    {

        private List<string> PreviousActions = new List<string> { };

        /**
         * Prompts the user for a routine and redirects the request
         */
        public Routine ProcessRoutine()
        {
        
            Console.WriteLine(Environment.NewLine + "Please insert the name of the routine you want to execute.");
            dynamic usr_payload = Console.ReadLine();

            if(!(usr_payload is string) && !(usr_payload is char)) ProcessRoutine();

            if(usr_payload == "addAutostart")
            {
                ActionController.AddApplicationToStartup();
                return AddRoutine("void");
            }
            if(usr_payload == "removeAutostart")
            {
                ActionController.RemoveApplicationFromStartup();
                return AddRoutine("void");
            }
 
            Routine routine = AddRoutine(usr_payload);

            if(routine.RoutineOrOptionExists())
            {
                
                return routine;

            }
            else ActionController.addError("The routine yout entered is not registered.");

            return AddRoutine("Error");

        } // public void ProcessRoutine()

        /**
         * Runs a program
         */
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

        /**
         * Executes a routine
         */
        public bool ExecuteRoutine(Routine routine, bool loadWebPageOnURLEncounter = true)
        {

            ResourceController RC = new ResourceController();

            string errors = null;
            if(routine.RoutineFiles.Count() <= 0) errors = "no_routinefiles_found;";
            
            foreach(string file in routine.RoutineFiles)
            {
                string filepath = RC.GetProgramPath(file.Trim());

                if(filepath.Contains("http://") || filepath.Contains("https://"))
                {
                    if(loadWebPageOnURLEncounter) System.Diagnostics.Process.Start(filepath.Trim());
                    continue;
                }
                else if(!ExecuteProgram(filepath)) ActionController.addError("An error occured while executing a program.");
            }

            return String.IsNullOrEmpty(errors) ? true : false;

        } // public bool ExecuteRoutine()

        /**
         * Structs a new routine
         */
        public static Routine AddRoutine(string routineName)
        {

            return new Routine(routineName);

        } // public static Routine AddRoutine()

        /**
         * Throws a custom erro
         */
        public static void addError(string msg)
        {

            if(msg.Length > 0)
            {

                Console.WriteLine(Environment.NewLine);

                try { misc.Utilities.WriteColoredLine(msg, ConsoleColor.Red); }
                finally { 
                    misc.Utilities.WriteColoredLine("Please press enter to proceed.", ConsoleColor.DarkGray);
                    Console.ReadLine();
                }
            }

        } // public static void addError(string msg)


        /**
         * Registers this app to the windows startup
         */
        public static void AddApplicationToStartup()
        {

            if(!System.Environment.OSVersion.ToString().Contains("Windows") && !System.Environment.OSVersion.ToString().Contains("Linux")) 
                return;

            string execPath = Directory.GetCurrentDirectory() + @"\DeveloperShortcut.exe";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("DeveloperShortcut", execPath);
            }

        } // public static void AddApplicationToStartup()

        /**
         * Removes this app from the windows startup
         */
        public static void RemoveApplicationFromStartup()
        {
            
            if(!System.Environment.OSVersion.ToString().Contains("Windows") && !System.Environment.OSVersion.ToString().Contains("Linux")) 
                return;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("DeveloperShortcut", false);
            }

        } // public static 

    }
}
