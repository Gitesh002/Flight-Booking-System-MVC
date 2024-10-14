using fbs.Data;
using fbs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace fbs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // --------------------------  User ---------------------------------- 

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // -------------------------- User Login Page ---------------------------------- 
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.UserAccounts.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.UserName);  // Store the username
                HttpContext.Session.SetInt32("UserId", user.UserAccountId);

                return RedirectToAction("Search", "Home"); // Redirect to search view after login
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        // --------------------------  Search Page ---------------------------------- 
        public IActionResult Search()
        {
            var airports = _context.Airports.ToList();  // Fetch airport data from the database
            return View(airports);
        }

        [HttpPost]
        public IActionResult SearchResults(string from, string to, DateTime? date)
        {
            // Fetch flights based on the search criteria
            var flights = _context.Flights
                          .Where(f => f.DepartureCity == from && f.ArrivalCity == to && f.DepartureDate.Date == date)
                          .ToList();

            var airports = _context.Airports
            .ToDictionary(a => a.IATA_code, a => $"{a.city_name} ({a.IATA_code})");

            var airport = _context.Airports.ToList();

            // Create the ViewModel and pass it to the view
            var viewModel = new FlightSearchViewModel
            {
                From = from,
                To = to,
                Date = date,
                Flights = flights,
                Airports = airports,
                ports = airport
            };  

            return View(viewModel);
        }


        // --------------------------- Reservations -------------------------


        public IActionResult Reserve(int flightId)
        {
            var flight = _context.Flights.Find(flightId);
            if (flight == null || flight.AvailableSeats <= 0)
            {
                return NotFound(); // Handle flight not found or no available seats
            }

            var airports = _context.Airports
            .ToDictionary(a => a.IATA_code, a => $"{a.city_name} ({a.IATA_code})");

            // Pass the flight details to the view
            ViewBag.Flight = flight; // Store the flight in ViewBag for access in the view
            ViewBag.Airports = airports;
            return View(); // Pass the reservation model to the view
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(int flightId, int numberOfSeats)
        {
            if (numberOfSeats <= 0)
            {
                ModelState.AddModelError("", "Please enter a valid number of seats.");
                return View(); // Return to the form if invalid
            }

            // Fetch the flight details using flightId
            var flight = _context.Flights.Find(flightId);

            if (flight == null)
            {
                ModelState.AddModelError("", "Flight not found.");
                return View(); 
            }

            if (flight.AvailableSeats < numberOfSeats)
            {
                ModelState.AddModelError("", "Not enough available seats.");
                return View(); // Return to the form if not enough seats are available
            }

            var username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.UserAccounts
              .FirstOrDefaultAsync(m => m.UserAccountId == userId);

            var airports = _context.Airports
            .ToDictionary(a => a.IATA_code, a => $"{a.city_name} ({a.IATA_code})");
    
            var model = new ReserveFlightViewModel
            {
                FlightId = flightId,
                ArrivalCity = flight.ArrivalCity,
                DepartureCity = flight.DepartureCity,
                DepartureDate = flight.DepartureDate,
                NumberOfSeats = numberOfSeats,
                PricePerSeat = flight.Price,
                Passengers = new List<Passenger>(new Passenger[numberOfSeats]),
                Airports = airports
            };

            return View("PassengerDetails", model);

            // Redirect to the booking confirmation page
            //return RedirectToAction("PaymentForm","Payments", new { revId = ticket.ReservationId });
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmReservation(ReserveFlightViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.UserAccounts
             .FirstOrDefaultAsync(m => m.UserAccountId == userId);

            var airports = _context.Airports
           .ToDictionary(a => a.IATA_code, a => $"{a.city_name} ({a.IATA_code})");

            var flights = await _context.Flights.FindAsync(model.FlightId);

            var reservation = new Reservation
            {
                FlightId = model.FlightId,
                UserId = user.UserAccountId,
                UserName = user.UserName,
                NumberOfSeats = model.NumberOfSeats,
                TotalCost = model.NumberOfSeats * model.PricePerSeat,
               // Airports = airports,

                ReservationDate = DateTime.Now,

                DepartureCityCode = flights.DepartureCity,
                ArrivalCityCode = flights.ArrivalCity,

                Passengers = model.Passengers.Select(p => new Passenger
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    Age = p.Age
                }).ToList()
            };

            if(reservation != null)
            {
                await _context.Reservations.AddAsync(reservation);
                _context.SaveChanges();
            }
                                                                                                            
            ViewBag.Airports = airports;
            // Redirect to a confirmation page or show success message
            return View("ReservationConfirm",reservation);
        }



























        // New action for booking confirmation
        //public async Task<IActionResult>ReservationPayment(int flightId, int numberOfSeats)
        //{
        //    var flight = _context.Flights.Find(flightId);

        //    ViewBag.Flight = flight;
        //    ViewBag.NumberOfSeats = numberOfSeats;
        //    ViewBag.Username = user.FirstName + " " + user.LastName;


        //    int revId = ticket.ReservationId;

        //    if(revId > 0) {

        //        return RedirectToAction("Payment", "Payments", new { reservationId = revId }); // Pass the flight and number of seats to the confirmation view
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //}


    }
}