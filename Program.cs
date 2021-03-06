﻿using DeveloperShortcut.controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DeveloperShortcut
{
    class Program
    {

        readonly public static string AppName = "Developer Shortcut";
        readonly public static string AppVersion = "1.1.0";
        readonly public static string AppAuthor = "Florian Götzrath";

        /**
         * Main function of the program
         */
        static void Main(string[] args)
        {

            Program.GetAppInfo();
            
            ActionController AC = new ActionController();
            ResourceController RC = new ResourceController();

            RC.LoadExecutableResources();
            RC.LoadRoutineResources();
            
            //AC.ExecuteProgram(AppDomain.CurrentDomain.BaseDirectory + @"\DeveloperShortcut.exe");

            List<string> ExecutableResources = RC.FileResources;
            List<string> RoutineResources = RC.RoutineResources;
            
            Routine routine = AC.ProcessRoutine();

            if(routine.RoutineName == "void")
            {
                AC.ExecuteProgram(System.Reflection.Assembly.GetEntryAssembly().Location);
                Environment.Exit(0);
            }

            if(!(AC.ExecuteRoutine(routine))) 
                ActionController.addError("An error occured while executing the given routine.");
            
        } // static void Main()

        /**
         * Outputs app related information
         */
        public static void GetAppInfo()
        {
            
            string msg = AppName + ": Version " + AppVersion + " by " + AppAuthor;
            ConsoleColor color = ConsoleColor.DarkCyan;

            misc.Utilities.WriteColoredLine(msg, color);

        } // static void GetAppInfo()

    }
}
