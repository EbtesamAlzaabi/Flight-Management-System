using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.MODELS
{
    public class Aircraft
    {
        public int aircraftId { get; set; }            // System Generated

        public string model { get; set; }              // User Input

        public int totalSeats { get; set; }            // User Input

        public bool isOperational { get; set; }        // User Input / Default Value = true
    }
}
