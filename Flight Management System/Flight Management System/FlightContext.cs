using Flight_Management_System.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System
{
    public class FlightContext
    {

        // Stores all passengers registered in the system
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();


        // Stores all pilots employed by the airline
        public List<Pilot> Pilots { get; set; } = new List<Pilot>();


        // Stores all aircraft available to the airline
        public List<Aircraft> Aircrafts { get; set; } = new List<Aircraft>();


        // Stores all scheduled flights
        public List<Flight> Flights { get; set; } = new List<Flight>();


        // Stores all passenger bookings
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
