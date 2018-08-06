using DeveloperShortcut.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperShortcut
{
    class Program
    {

        readonly public static string AppName = "Developer Shortcut";
        readonly public static string AppVersion = "1.0.0";
        readonly public static string AppAuthor = "Florian Götzrath";

        // Main function of the program
        static void Main(string[] args)
        {

            GetAppInfo();

            ActionController AC = new ActionController();
            ResourceController RC = new ResourceController();

            RC.LoadExecutableResources();
            RC.LoadRoutineResources();

            List<string> ExecutableResources = RC.FileResources;
            List<string> RoutineResources = RC.RoutineResources;

            int counter = 0;
            foreach(var resource in ExecutableResources)
            {
                counter++;
                if(counter == 2) Console.WriteLine(misc.Utilities.StrExplode(':', resource)[2]);
                if(counter == 2) AC.ExecuteProgram(misc.Utilities.StrExplode(':', resource)[2]);
            }
            
        } // static void Main()

        // Outputs the app information
        public static void GetAppInfo()
        {
            
            string msg = AppName + ": Version " + AppVersion + " by " + AppAuthor;
            ConsoleColor color = ConsoleColor.Green;

            misc.Utilities.WriteColoredLine(msg, color);

        } // static void GetAppInfo()

    }
}
