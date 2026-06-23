using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.MODELS
{
    public class Passenger
    {
        public int passengerId { get; set; }           // System Generated

        public string passengerName { get; set; }      // User Input

        public string passengerEmail { get; set; }     // User Input

        public string passengerPhone { get; set; }     // User Input

        public string passportNumber { get; set; }     // User Input (Must be Unique)

        public string nationality { get; set; }        // User Input
    }
}
