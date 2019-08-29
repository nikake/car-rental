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
    public class CarsController : Controller
    {
        private readonly ICarRepository carRepository;

        public CarsController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> Index()
        {
            var cars = await carRepository.GetAll();
            return cars;
        }

        [HttpGet("{id}")]
        public async Task<Car> Index(int id)
        {
            var car = await carRepository.GetById(id);
            return car;
        }

        [Route("unavailable")]
        [HttpGet]
        public async Task<IEnumerable<Car>> Unavailable()
        {
            var cars = await carRepository.GetAllUnavailableCars();
            return cars;
        }
    }
}