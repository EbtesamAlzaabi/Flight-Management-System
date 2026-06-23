using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.MODELS
{
    public class Booking
    {
        public int bookingId { get; set; }             // System Generated

        public int passengerId { get; set; }           // User Selects Existing Passenger

        public int flightId { get; set; }              // User Selects Existing Flight

        public string seatNumber { get; set; }         // System Generated / Assigned at Booking

        public string bookingDate { get; set; }        // System Generated (Current Date)

        public decimal totalPrice { get; set; }        // System Calculated from Flight.ticketPrice

        public string status { get; set; }             // Default Value = "Confirmed"
                                                       // Confirmed | Cancelled
    }
}
