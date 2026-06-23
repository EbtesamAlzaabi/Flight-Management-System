using Flight_Management_System.MODELS;
using Microsoft.Win32;
using System.Numerics;

namespace Flight_Management_System
{
    internal class Program
    {


        //system storage ( actual storage in memory for all lists ) 
        public static FlightContext context = new FlightContext()
        {
            Passengers = new List<Passenger>(),
            Pilots = new List<Pilot>(),
            Aircrafts = new List<Aircraft>(),
            Flights = new List<Flight>(),
            Bookings = new List<Booking>()
        };


        // ─────────────────────────────────────────────────────────────
        // EASY 01 — Register a Passenger
        // touches: Passengers
        // ─────────────────────────────────────────────────────────────
        public static void RegisterPassenger()
        {
            Console.WriteLine("\n=== Register Passenger ===");

            Console.Write("Enter Passenger Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Passenger Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Passenger Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Passport Number: ");
            string passportNumber = Console.ReadLine();

            Console.Write("Enter Nationality: ");
            string nationality = Console.ReadLine();


            // Check duplicate passport
            bool exists = context.Passengers
                .Any(p => p.passportNumber == passportNumber);


            if (exists)
            {
                Console.WriteLine("Passenger already exists.");
                return;
            }


            // Generate Passenger ID

            int passengerId = 1;

            if (context.Passengers.Any())
            {
                passengerId = context.Passengers
                    .Max(p => p.passengerId) + 1;
            }


            Passenger passenger = new Passenger
            {
                passengerId = passengerId,
                passengerName = name,
                passengerEmail = email,
                passengerPhone = phone,
                passportNumber = passportNumber,
                nationality = nationality
            };


            // Add to List
            context.Passengers.Add(passenger);


            // Confirmation
            Console.WriteLine(
                $"Passenger added successfully. Assigned ID: {passengerId}"
            );
        }

        static void Main(string[] args)
        {

            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("   Flight Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1  - Register a Passenger");
                Console.WriteLine(" 2  - Add an Aircraft");
                Console.WriteLine(" 3  - Register a Pilot");
                Console.WriteLine(" 4  - View All Flights");
                Console.WriteLine(" 5  - Schedule a Flight");
                Console.WriteLine(" 6  - Book a Flight");
                Console.WriteLine(" 7  - Cancel a Booking");
                Console.WriteLine(" 8  - Depart a Flight");
                Console.WriteLine(" 9  - Cancel a Flight");
                Console.WriteLine(" 10 - Passenger Booking History");
                Console.WriteLine(" 11 - Flight Revenue & Load Factor Report");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: RegisterPassenger(); break;
                    //case 2: AddAircraft(); break;
                    //case 3: RegisterPilot(); break;
                    //case 4: ViewAllFlights(); break;
                    //case 5: ScheduleFlight(); break;
                    //case 6: BookFlight(); break;
                    //case 7: CancelBooking(); break;
                    //case 8: DepartFlight(); break;
                    //case 9: CancelFlight(); break;
                    //case 10: PassengerBookingHistory(); break;
                    //case 11: FlightRevenueLoadFactorReport(); break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Goodbye!");
        }

    }
}
