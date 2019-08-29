using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingRepository bookingRepository;
        public const double DEFAULT_PRICE = 0.0;

        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Booking>> Index()
        {
            return await bookingRepository.GetAll();
        }

        [HttpPost]
        [Route("add")]
        public async Task<bool> Add([FromBody] Booking booking)
        {
            return await bookingRepository.Create(booking);
        }

        [Route("update")]
        [HttpPost]
        public async Task<bool> Update([FromBody] Booking booking)
        {
            return await bookingRepository.Update(booking);
        }

        [HttpGet("{id}")]
        public async Task<Booking> Index(int id)
        {
            return await bookingRepository.GetById(id);
        }

        [Route("price")]
        [HttpPost]
        public double GetPrice([FromBody] Booking booking)
        {
            var car = booking.Car;
            int days = (int)Math.Round((booking.ReturnRegistration.DateTime.Date - booking.PickUpRegistration.DateTime.Date).TotalDays);
            int distance = booking.ReturnRegistration.DistanceMeter - booking.PickUpRegistration.DistanceMeter;
            switch (car.CarType)
            {
                case CarType.Small: return car.BaseDayPrice * days;
                case CarType.Estate: return car.BaseDayPrice * days * 1.3 + car.BaseKmPrice * distance;
                case CarType.Truck: return car.BaseDayPrice * days * 1.5 + car.BaseKmPrice * distance * 1.5;
                default: return DEFAULT_PRICE;
            }
        }
    }
}