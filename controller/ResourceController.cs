using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DeveloperShortcut.controller
{
    class ResourceController
    {
        
        readonly public static string ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        readonly public static string ResourcePath = Path.Combine(ProjectPath, "resource\\");
        readonly protected static string FilesResourcePath = Path.Combine(ResourcePath, "Files.json");
        readonly protected static string RoutinesResourcePath = Path.Combine(ResourcePath, "Routines.json");

        public List<string> FileResources = new List<string>();
        public List<string> RoutineResources = new List<string>();

        /**
         * Loads any file resources from the dedicated file into the FileResources Array
         */
        public void LoadExecutableResources()
        {

            JObject fileContent = misc.Utilities.LoadJSON(FilesResourcePath);
            dynamic dynJson = JsonConvert.DeserializeObject(fileContent.ToString());

            foreach (var block in dynJson)
            {
                string[] lines = block.ToString().Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );

                foreach(string line in lines)
                {
                    if (misc.Utilities.ContainsAny(line, new string[] { "{", "}" })) continue;

                    FileResources.Add(line);
                }
                
            }

        } // public static void LoadExecutableResources()

        /**
         * Loads any file resources from the dedicated file into the RoutineResources Array
         */
        public void LoadRoutineResources()
        {

            if (FileResources == null || FileResources.Count <= 0) LoadExecutableResources();

            JObject fileContent = misc.Utilities.LoadJSON(RoutinesResourcePath);
            dynamic dynJson = JsonConvert.DeserializeObject(fileContent.ToString());

            foreach(var block in dynJson)
            {
                string[] lines = block.ToString().Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );

                foreach(string line in lines)
                {
                    if (misc.Utilities.ContainsAny(line, new string[] { "{", "}" })) continue;

                    RoutineResources.Add(line);
                }

            }

        } // public static void LoadRoutineResources()

        /**
         * Returns a list with program names that are listed in a given routine
         */
        public string[] GetRoutineFiles(string routineName)
        {

            if(RoutineResources == null || RoutineResources.Count == 0)
            {
                LoadExecutableResources();
                LoadRoutineResources();
                GetRoutineFiles(routineName);
            }

            foreach(string line in RoutineResources)
            {
                string cleanline = line.Replace("\"", "");
                cleanline = cleanline.Replace(",", "");
                
                string lineName = misc.Utilities.StrExplode(':', cleanline)[0].ToLower().Trim();

                if(lineName.Equals(routineName.ToLower().Trim()))
                {

                    string[] lineFiles = misc.Utilities.StrExplode(';', misc.Utilities.StrExplode(':', cleanline)[1]);

                    return lineFiles;

                }
            }

            return new string[] {  };
             
        } // public Array GetRoutineConfigByName()

         /**
         * Returns the configured path to a given program
         */
        public string GetProgramPath(string programName)
        {

            if (FileResources == null || FileResources.Count <= 0) LoadExecutableResources();

            foreach(var line in this.FileResources)
            {
                if(line.Contains(programName)) 
                    return misc.Utilities.TransferResourceToProgram(line).Replace("\"", "");
            }

            ActionController.addError("No valid path was found for the program " + programName.Trim() + " found.");
            return "";

        } // public static string getProgramPath(string programName)

    }
}
