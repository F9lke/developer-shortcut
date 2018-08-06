using DeveloperShortcut.controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperShortcut.misc
{
    class Utilities
    {

        // Reads a JSON file and returns its content in an array
        public static JObject LoadJSON(string pathToFile)
        {

            if(!pathToFile.Contains("\\") && !pathToFile.Contains("/") && pathToFile.Length <= 10)
                pathToFile = ResourceController.ResourcePath + pathToFile;

            using (StreamReader r = new StreamReader(pathToFile))
            {

                string json = r.ReadToEnd();
                dynamic content = JsonConvert.DeserializeObject(json);

                return content;

            }

        } // public static Array LoadJSON()

        // Checks if a string contains at least one of the needle strings
        public static bool ContainsAny(string haystack, string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle)) return true;
            }

            return false;

        } // public static bool ContainsAny()

        // Outputs a colored line in the cli
        public static void WriteColoredLine(string msg, ConsoleColor color)
        {

            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();

        } // static void WriteColoredLine()

        // Explodes a string
        public static string[] StrExplode(char delimiter, string target)
        {

            string[] explodedStr = { };

            if (delimiter.ToString() == "\n")
            {
                explodedStr = target.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );
            }
            else
            {
                explodedStr = target.Split(delimiter);
            }

            return explodedStr;

        } // public static List<string> StrExplode()

    }
}
