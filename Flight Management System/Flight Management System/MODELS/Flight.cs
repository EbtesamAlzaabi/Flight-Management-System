using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.MODELS
{
    public class Flight
    {
        public int flightId { get; set; }              // System Generated

        public string flightCode { get; set; }         // System Generated (e.g. OA-201)

        public int aircraftId { get; set; }            // User Selects Existing Aircraft

        public int pilotId { get; set; }               // User Selects Available Pilot

        public string origin { get; set; }             // User Input

        public string destination { get; set; }        // User Input

        public string departureDate { get; set; }      // User Input

        public string departureTime { get; set; }      // User Input

        public decimal ticketPrice { get; set; }       // User Input

        public int availableSeats { get; set; }        // System Calculated from Aircraft.totalSeats

        public int flightDuration { get; set; }        // Duration in Hours

        public string status { get; set; }             // Default Value = "Scheduled"
                                                       // Scheduled | Departed | Cancelled
    }
}
