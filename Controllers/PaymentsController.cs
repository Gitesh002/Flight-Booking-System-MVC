using fbs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using fbs.Data;
using Newtonsoft.Json;

public class PaymentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PaymentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult PaymentForm(int revId, List<Passenger> Passengers)
    {
        var rev = _context.Reservations.Find(revId);
        var flight = _context.Flights.Find(rev.FlightId);

        rev.Passengers = Passengers;
        
        if (rev == null)
        {
            return NotFound(); // Handle flight not found or no available seats
        }

        var payment = new Payment()
        {
            ReservationId = revId
        };

        // Pass the flight details to the view
        ViewBag.ticket = rev;
        ViewBag.flight = flight; // Store the flight in ViewBag for access in the view
        return View(); // Pass the reservation model to the view
    }

    [HttpPost]
    public async Task<IActionResult> PaymentForm(int revId, string PaymentMethod , List<Passenger> Passengers)
    {
        var rev = _context.Reservations.Find(revId);
        rev.Passengers = Passengers;
        var flight = _context.Flights.Find(rev.FlightId);
        var Paycheck = new Payment()
        {
            ReservationId = revId,
            PaymentMethod = PaymentMethod,
            Amount = rev.TotalCost,
            PaymentDate = DateTime.Now
        };

        if(Paycheck != null) 
        {
            _context.Add(Paycheck);
            await _context.SaveChangesAsync();
        }


        TempData["ticket"] = revId; // Store IDs in TempData, and retrieve objects later
        TempData["flight"] = flight.FlightId;
        TempData["pay"] = Paycheck.PaymentId;
        TempData["Passengers"] = JsonConvert.SerializeObject(Passengers);

        return RedirectToAction("PaymentConfirmed");
    }


    public IActionResult PaymentConfirmed()
    {
        var ticketId = TempData["ticket"] as int?;
        var flightId = TempData["flight"] as int?;
        var paymentId = TempData["pay"] as int?;
        var passengersJson = TempData["Passengers"] as string;
        var passengers = JsonConvert.DeserializeObject<List<Passenger>>(passengersJson);

        if (ticketId == null || flightId == null || paymentId == null)
        {
            return NotFound();
        }

        // Retrieve the actual objects using the IDs from TempData
        var ticket = _context.Reservations.Find(ticketId);

        ticket.Passengers = passengers;

        var flight = _context.Flights.Find(flightId);
        var payment = _context.Payments.Find(paymentId);

        //Update the flight's available seats
        flight.AvailableSeats -= ticket.NumberOfSeats;

        // Save changes to the database
        _context.SaveChanges();


        // Store the actual objects in ViewBag to display in the view
        ViewBag.ticket = ticket;
        ViewBag.flight = flight;
        ViewBag.payment = payment;

        return View();
    }




}
