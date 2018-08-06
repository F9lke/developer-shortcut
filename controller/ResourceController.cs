using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Loads any file resources from dedicated file into the FileResources Array
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

        // Loads any routine resources from dedicated file into the RoutineResources Array
        public void LoadRoutineResources()
        {

            if (FileResources == null || FileResources.Count == 0) LoadExecutableResources();

        } // public static void LoadRoutineResources()

        // Returns an the configured settings of a routine
        public Array GetRoutineConfigByName(string routineName)
        {

            if(RoutineResources == null || RoutineResources.Count == 0)
            {
                LoadExecutableResources();
                LoadRoutineResources();
                GetRoutineConfigByName(routineName);
            }

            return new string[,] { };

        } // public Array GetRoutineConfigByName()

    }
}
