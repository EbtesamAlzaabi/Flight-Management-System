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

        // ─────────────────────────────────────────────────────────────
        // EASY 02 — Add an Aircraft
        // touches: Aircrafts
        // ─────────────────────────────────────────────────────────────
        public static void AddAircraft()
        {
            Console.WriteLine("\n=== Add Aircraft ===");

            // Read aircraft model from user
            Console.Write("Enter Aircraft Model: ");
            string model = Console.ReadLine();

            // Read total seat capacity
            Console.Write("Enter Total Seats: ");
            int totalSeats = int.Parse(Console.ReadLine());

            // Generate a new unique aircraft ID using LINQ /*Ternary Operator*/
            //إذا توجد طائرات في النظام، أعطني أكبر رقم طائرة وزيد عليه 1، وإلا أعطني الرقم 1.//
            int newAircraftId = context.Aircrafts.Any()
                ? context.Aircrafts.Max(a => a.aircraftId) + 1
                : 1;

            // Create and add the aircraft to the system
            context.Aircrafts.Add(
                new Aircraft
                {
                    aircraftId = newAircraftId,
                    model = model,
                    totalSeats = totalSeats,

                    // New aircraft starts operational
                    isOperational = true
                });

            // Confirm successful addition
            Console.WriteLine("\nAircraft added successfully.");

            Console.WriteLine($"Assigned Aircraft ID: {newAircraftId}");

        }

        // ─────────────────────────────────────────────────────────────
        // EASY 03 — Register a Pilot
        // touches: Pilots
        // ─────────────────────────────────────────────────────────────
        public static void RegisterPilot()
        {
            Console.WriteLine("\n=== Register Pilot ===");

            // Read pilot details
            Console.Write("Enter Pilot Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Pilot Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter License Number: ");
            string licenseNumber = Console.ReadLine();

            // LINQ: Check if another pilot already uses this license number
            bool exists = context.Pilots.Any(p => p.licenseNumber == licenseNumber);

            if (exists)
            {
                Console.WriteLine("A pilot with this license number already exists.");
                return;
            }

            // LINQ: Generate a new Pilot ID: max existing ID + 1, or 1 if no pilots exist//
            int newPilotId = context.Pilots.Any()
                ? context.Pilots.Max(p => p.pilotId) + 1
                : 1;

            // Add the new pilot
            context.Pilots.Add(
                new Pilot
                {
                    pilotId = newPilotId,
                    pilotName = name,
                    pilotPhone = phone,
                    licenseNumber = licenseNumber,

                    // New pilot starts with no flight hours
                    flightHours = 0,

                    // Pilot is available for flight assignment
                    isAvailable = true
                });

            // Confirm registration and display Pilot ID
            Console.WriteLine("\nPilot registered successfully.");
            Console.WriteLine($"Assigned Pilot ID: {newPilotId}");
        }

        // ─────────────────────────────────────────────────────────────
        // EASY 04 — View All Flights
        // touches: Flights
        // ─────────────────────────────────────────────────────────────
        public static void ViewAllFlights()
        {
            Console.WriteLine("\n=== All Flights ===");

            // Check if there are no flights in the system
            if (!context.Flights.Any())
            {
                Console.WriteLine("No flights found.");
                return;
            }

            // Display all flight details
            foreach (Flight f in context.Flights)
            {
                Console.WriteLine($"Code: {f.flightCode}  |  Origin: {f.origin}  |  Destination: {f.destination}" +
                                  $"  |  Date: {f.departureDate}  |  Time: {f.departureTime}" +
                                  $"  |  Available Seats: {f.availableSeats}  |  Ticket Price: {f.ticketPrice}" +
                                  $"  |  Status: {f.status}");
            }
        }
        // ─────────────────────────────────────────────────────────────
        // MEDIUM 05 — Schedule a Flight
        // touches: Flights, Aircrafts, Pilots
        // ─────────────────────────────────────────────────────────────
        public static void ScheduleFlight()
        {
            Console.WriteLine("\n=== Schedule Flight ===");

            // Read aircraft ID from user
            Console.Write("Enter Aircraft ID: ");
            int aircraftId = int.Parse(Console.ReadLine());

            // Read Flight Duration
            Console.Write("Enter Flight Duration (Hours): ");
            int flightDuration = int.Parse(Console.ReadLine());


            // LINQ: Find the selected aircraft
            // يبحث داخل قائمة الطائرات عن الطائرة التي رقمها يساوي الرقم الذي أدخله المستخدم .//
            Aircraft aircraft =
                context.Aircrafts
                .FirstOrDefault(a => a.aircraftId == aircraftId);

            // Ensure aircraft exists ** التأكد أن الطائرة موجودة//
            if (aircraft == null)
            {
                Console.WriteLine("Aircraft not found.");
                return;
            }

            // Aircraft must be operational **التأكد أن الطائرة جاهزة للعمل//
            if (aircraft.isOperational == false)
            {
                Console.WriteLine("Aircraft is not operational.");
                return;
            }

            // Read pilot ID
            Console.Write("Enter Pilot ID: ");
            int pilotId = int.Parse(Console.ReadLine());


            // LINQ: Find the selected pilot * يبحث عن الطيار اذ موجود بقائمة الطيارين  //

            Pilot pilot =
                context.Pilots
                .FirstOrDefault(p => p.pilotId == pilotId);

            // Ensure pilot exists * يتاكد ان الطيار موجود //
            if (pilot == null)
            {
                Console.WriteLine("Pilot not found.");
                return;
            }

            // Pilot must be available for assignment * هل الطيار متاح ؟//
            if (pilot.isAvailable == false)
            {
                Console.WriteLine("Pilot is not available.");
                return;
            }

            // Read flight details
            Console.Write("Enter Origin: ");
            string origin = Console.ReadLine();

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter Departure Date: ");
            string departureDate = Console.ReadLine();

            Console.Write("Enter Departure Time: ");
            string departureTime = Console.ReadLine();

            Console.Write("Enter Ticket Price: ");
            decimal ticketPrice = decimal.Parse(Console.ReadLine());

            // LINQ: Generate next flight ID
            int flightId =
                context.Flights.Any()
                ? context.Flights.Max(f => f.flightId) + 1
                : 1;

            // Automatically generate flight code
            string flightCode = "Flight is -" + flightId;

            // Create the new flight record
            context.Flights.Add(
                new Flight
                {
                    flightId = flightId,
                    flightCode = flightCode,
                    aircraftId = aircraftId,
                    pilotId = pilotId,
                    origin = origin,
                    destination = destination,
                    departureDate = departureDate,
                    departureTime = departureTime,
                    ticketPrice = ticketPrice,
                    flightDuration = flightDuration,

                    // Available seats come from the selected aircraft
                    availableSeats = aircraft.totalSeats,

                    // New flights start as Scheduled
                    status = "Scheduled"
                });

            // Pilot is now assigned to a flight
            pilot.isAvailable = false;

            // Display confirmation
            Console.WriteLine("\nFlight Scheduled Successfully.");
            Console.WriteLine($"Flight Code: {flightCode}");
        }

        // ─────────────────────────────────────────────────────────────
        // MEDIUM 06 — Book a Flight
        // touches: Passengers, Flights, Bookings
        // ─────────────────────────────────────────────────────────────

        public static void BookFlight()
        {
            Console.WriteLine("\n=== Book Flight ===");

            // Read passenger ID
            Console.Write("Enter Passenger ID: ");
            int passengerId = int.Parse(Console.ReadLine());


            // LINQ: Find passenger by ID
            Passenger passenger =
                context.Passengers
                .FirstOrDefault(p => p.passengerId == passengerId);


            // Read destination
            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();


            // LINQ: Find scheduled flights with available seats
            var flights =
                context.Flights
                .Where(f =>
                    f.destination == destination &&
                    f.status == "Scheduled" &&
                    f.availableSeats > 0)
                .ToList();


            // Check flights
            if (flights.Count == 0)
            {
                Console.WriteLine("No available flights found.");
                return;
            }


            // Display flights
            foreach (Flight flight in flights)
            {
                Console.WriteLine(
                    $"{flight.flightId} - {flight.flightCode} | " +
                    $"{flight.departureDate} | " +
                    $"{flight.departureTime} | " +
                    $"Seats: {flight.availableSeats}");
            }


            // Passenger selects a flight
            Console.Write("\nEnter Flight ID: ");
            int flightId = int.Parse(Console.ReadLine());


            // LINQ: Find selected flight
            var selectedFlight =
                context.Flights
                .FirstOrDefault(f => f.flightId == flightId);


            // Check if selected flight exists
            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }


            // Generate new Booking ID
            int bookingId =
                context.Bookings.Count > 0
                ? context.Bookings.Max(b => b.bookingId) + 1
                : 1;


            // Assign seat label automatically
            string seatNumber =
                "A" + selectedFlight.availableSeats;


            // Create new booking record 
            context.Bookings.Add(
                new Booking
                {
                    bookingId = bookingId,

                    // Link booking with passenger
                    passengerId = passengerId,

                    // Link booking with flight
                    flightId = flightId,

                    // Assigned seat
                    seatNumber = seatNumber,

                    // Current booking date
                    bookingDate = DateTime.Now.ToShortDateString(),

                    // Price comes from flight ticket price
                    totalPrice = selectedFlight.ticketPrice,

                    // New booking starts confirmed
                    status = "Confirmed"
                });


            // Reduce available seats after booking
            selectedFlight.availableSeats--;


            // Confirm successful booking
            Console.WriteLine("\nBooking Created Successfully.");

            Console.WriteLine($"Booking ID: {bookingId}");
            Console.WriteLine($"Seat Number: {seatNumber}");
            Console.WriteLine($"Total Price: {selectedFlight.ticketPrice}");
        }



        // ─────────────────────────────────────────────────────────────
        // MEDIUM 07 — Cancel a Booking
        // touches: Bookings, Flights
        // ─────────────────────────────────────────────────────────────
        public static void CancelBooking()
        {
            Console.WriteLine("\n=== Cancel Booking ===");


            Console.Write("Enter Booking ID: ");
            int bookingId = int.Parse(Console.ReadLine());

            // Find Booking
            var booking =
                context.Bookings
                .FirstOrDefault(b => b.bookingId == bookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            if (booking.status == "Cancelled")
            {
                Console.WriteLine("Booking is already cancelled.");
                return;
            }

            // Find Flight
            var flight =
                context.Flights
                .FirstOrDefault(f => f.flightId == booking.flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (flight.status == "Departed")
            {
                Console.WriteLine("Cannot cancel. Flight already departed.");
                return;
            }

            // Cancel Booking
            booking.status = "Cancelled";

            // Return Seat
            flight.availableSeats++;

            Console.WriteLine("\nBooking Cancelled Successfully.");


        }


        // ─────────────────────────────────────────────────────────────
        // MEDIUM 08 — Depart a Flight
        // touches: Flights, Pilots
        // ─────────────────────────────────────────────────────────────
        public static void DepartFlight()
        {
            Console.WriteLine("\n=== Depart Flight ===");

            // Read Flight ID
            Console.Write("Enter Flight ID: ");
            int flightId = int.Parse(Console.ReadLine());


            // Find Flight
            Flight flight = context.Flights
                .FirstOrDefault(f => f.flightId == flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            // Check Status
            if (flight.status == "Departed")
            {
                Console.WriteLine("Flight already departed.");
                return;
            }

            if (flight.status == "Cancelled")
            {
                Console.WriteLine("Cancelled flight cannot depart.");
                return;
            }

            // Find Assigned Pilot
            Pilot pilot = context.Pilots
                .FirstOrDefault(p => p.pilotId == flight.pilotId);

            if (pilot == null)
            {
                Console.WriteLine("Pilot not found.");
                return;
            }

            // Update Pilot Flight Hours
            pilot.flightHours += flight.flightDuration;

            // Mark Flight as Departed
            flight.status = "Departed";

            // Confirm Departure
            Console.WriteLine("\nFlight departed successfully.");
            Console.WriteLine($"Flight Status: {flight.status}");
            Console.WriteLine($"Pilot Total Flight Hours: {pilot.flightHours}");
        }

        // ─────────────────────────────────────────────────────────────
        // MEDIUM 09 — Cancel a Flight
        // touches: Flights, Bookings, Pilots
        // ─────────────────────────────────────────────────────────────
        public static void CancelFlight()
        {
            Console.WriteLine("\n=== Cancel Flight ===");

            // Read Flight ID
            Console.Write("Enter Flight ID: ");
            int flightId = int.Parse(Console.ReadLine());

            // Find Flight
            Flight flight =
                context.Flights
                .FirstOrDefault(f => f.flightId == flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (flight.status == "Cancelled")
            {
                Console.WriteLine("Flight already cancelled.");
                return;
            }

            if (flight.status == "Departed")
            {
                Console.WriteLine("Departed flight cannot be cancelled.");
                return;
            }

            // Cancel Flight
            flight.status = "Cancelled";

            // Find All Confirmed Bookings
            var bookings =
                context.Bookings
                .Where(b =>
                    b.flightId == flight.flightId &&
                    b.status == "Confirmed")
                .ToList();

            // Count Affected Bookings
            int affectedBookings = bookings.Count;

            // Cancel All Bookings
            foreach (var booking in bookings)
            {
                booking.status = "Cancelled";
            }

            // Make Pilot Available Again
            Pilot pilot =
                context.Pilots
                .FirstOrDefault(p => p.pilotId == flight.pilotId);

            if (pilot != null)
            {
                pilot.isAvailable = true;
            }

            // Confirmation
            Console.WriteLine("\nFlight cancelled successfully.");
            Console.WriteLine($"Affected Bookings: {affectedBookings}");
        }


        // ─────────────────────────────────────────────────────────────
        // HARD 10 — Passenger Booking History
        // touches: Passengers, Bookings, Flights
        // ─────────────────────────────────────────────────────────────
        public static void PassengerBookingHistory()
        {
            Console.WriteLine("\n=== Passenger Booking History ===");

            Console.Write("Enter Passenger ID: ");
            int passengerId = int.Parse(Console.ReadLine());

            // Find Passenger
            var passenger =
                context.Passengers
                .FirstOrDefault(p => p.passengerId == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            // Get Passenger Bookings *
            // هات جميع حجوزات هذا الراكب، ثم اربط كل حجز بالرحلة الخاصة به،
            // ثم اعرض المعلومات المطلوبة من الجدولين معًا(Booking + Flight ).
            var bookings = context.Bookings
                .Where(b => b.passengerId == passengerId)
                .ToList();

            if (!bookings.Any())
            {
                Console.WriteLine("No bookings found.");
                return;
            }


            Console.WriteLine("\n=== Booking History ===");

            foreach (var booking in bookings)
            {
                Flight flight = context.Flights
                    .FirstOrDefault(f => f.flightId == booking.flightId);


                Console.WriteLine("\n--------------------------------");

                Console.WriteLine($"Flight Code: {flight.flightCode}");
                Console.WriteLine($"Origin: {flight.origin}");
                Console.WriteLine($"Destination: {flight.destination}");
                Console.WriteLine($"Departure Date: {flight.departureDate}");

                Console.WriteLine($"Seat Number: {booking.seatNumber}");
                Console.WriteLine($"Price Paid: {booking.totalPrice}");
                Console.WriteLine($"Booking Status: {booking.status}");
            }


            // LINQ: Calculate total spent on confirmed bookings only
            // هات جميع الحجوزات الخاصة بهذا الراكب، وخذ منها الحجوزات المؤكدة فقط،
            // ثم اجمع أسعارها واحفظ الناتج في totalSpent

            decimal totalSpent =
                context.Bookings
                .Where(b =>
                    b.passengerId == passengerId &&
                    b.status == "Confirmed")
                .Sum(b => b.totalPrice);

            Console.WriteLine("\n================================");

            Console.WriteLine($"Total Amount Spent: {totalSpent}");
        }


        // ─────────────────────────────────────────────────────────────
        // HARD 11 — Flight Revenue & Load Factor Report
        // touches: Flights, Bookings, Aircrafts
        // ─────────────────────────────────────────────────────────────
        public static void FlightRevenueLoadFactorReport()
        {
            Console.WriteLine("\n=== Flight Revenue & Load Factor Report ===");

            var report =
                from flight in context.Flights

                join aircraft in context.Aircrafts
                on flight.aircraftId equals aircraft.aircraftId

                let confirmedBookings =
                    context.Bookings
                    .Where(b =>
                        b.flightId == flight.flightId &&
                        b.status == "Confirmed")

                let totalBookings = confirmedBookings.Count()

                let totalRevenue = confirmedBookings.Sum(b => b.totalPrice)

                let loadFactor =
                    aircraft.totalSeats > 0
                    ? ((double)totalBookings / aircraft.totalSeats) * 100
                    : 0

                orderby totalRevenue descending

                select new
                {
                    flight.flightCode,

                    Route = flight.origin + " -> " + flight.destination,

                    TotalBookings = totalBookings,

                    TotalRevenue = totalRevenue,

                    LoadFactor = loadFactor
                };

            if (!report.Any())
            {
                Console.WriteLine("No flights found.");
                return;
            }

            foreach (var item in report)
            {
                Console.WriteLine("\n--------------------------------");

                Console.WriteLine($"Flight Code: {item.flightCode}");

                Console.WriteLine($"Route: {item.Route}");

                Console.WriteLine($"Confirmed Bookings: {item.TotalBookings}");

                Console.WriteLine($"Revenue: {item.TotalRevenue}");

                Console.WriteLine($"Load Factor: {item.LoadFactor:F2}%");
            }

            // LINQ: Grand Total Revenue Across All Flights
            decimal grandTotalRevenue =
                context.Bookings
                .Where(b => b.status == "Confirmed")
                .Sum(b => b.totalPrice);

            Console.WriteLine("\n================================");

            Console.WriteLine($"Grand Total Revenue: {grandTotalRevenue}");
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
                        case 2: AddAircraft(); break;
                        case 3: RegisterPilot(); break;
                        case 4: ViewAllFlights(); break;
                        case 5: ScheduleFlight(); break;
                        case 6: BookFlight(); break;
                        case 7: CancelBooking(); break;
                        case 8: DepartFlight(); break;
                        case 9: CancelFlight(); break;
                        case 10: PassengerBookingHistory(); break;
                        case 11: PassengerBookingHistory(); break;
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

