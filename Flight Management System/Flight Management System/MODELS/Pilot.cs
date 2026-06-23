using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.MODELS
{
    public class Pilot
    {
        public int pilotId { get; set; }               // System Generated

        public string pilotName { get; set; }          // User Input

        public string pilotPhone { get; set; }         // User Input

        public string licenseNumber { get; set; }      // User Input

        public int flightHours { get; set; }           // Default Value = 0, Updated by System

        public bool isAvailable { get; set; }          // Default Value = true, Updated by System
    }
}
