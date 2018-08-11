using DeveloperShortcut.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperShortcut
{
    class Routine
    { 

        public string RoutineName = "";
        public List<string> RoutineFiles = new List<string> { };

        // Constructor for the Routine class
        public Routine(string routineName)
        {

            ResourceController RC = new ResourceController();

            RoutineName = routineName;
            dynamic RoutineConfig = RC.GetRoutineConfigByName(RoutineName);

        } // public Routine()

        // Checks if a routine or an option exists
        public bool RoutineOrOptionExists()
        {

            ResourceController RC = new ResourceController();
            RC.LoadRoutineResources();
            List<string> Resources = RC.RoutineResources;

            foreach(var resource in Resources)
            {
                if (resource.Contains(RoutineName)) return true;
            }

            return false;

        } // public static bool RoutineExists()

    }
}
