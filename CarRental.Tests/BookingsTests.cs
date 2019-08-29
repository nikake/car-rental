using CarRental.Database;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Web.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests
{
    public class BookingsControllerTests
    {
        private readonly Mock<ICustomerRepository> customerRepository;
        private readonly Mock<IBookingRepository> bookingRepository;
        private readonly Mock<ICarRepository> carRepository;
        private readonly Mock<IRegistrationRepository> registrationRepository;
        private readonly BookingController bookingController;
        private readonly CarsController carsController;

        #region MockObjects

        private Car[] mockCars = new Car[]
        {
            new Car{ Id = 1, RegistrationNumber="ABC123", CarType = CarType.Small, Status = Status.Available, BaseDayPrice = 400, BaseKmPrice = 0, DistanceMeter = 1190 },
            new Car{ Id = 2, RegistrationNumber="CBA323", CarType = CarType.Small, Status = Status.Available, BaseDayPrice = 420, BaseKmPrice = 0, DistanceMeter = 1750 },
            new Car{ Id = 3, RegistrationNumber="AAA313", CarType = CarType.Small, Status = Status.Available, BaseDayPrice = 400, BaseKmPrice = 0, DistanceMeter = 1500 },
            new Car{ Id = 4, RegistrationNumber="BBB123", CarType = CarType.Estate, Status = Status.Unavailable, BaseDayPrice = 480, BaseKmPrice = 6, DistanceMeter = 750 },
            new Car{ Id = 5, RegistrationNumber="CCC123", CarType = CarType.Estate, Status = Status.Available, BaseDayPrice = 370, BaseKmPrice = 8, DistanceMeter = 350 },
            new Car{ Id = 6, RegistrationNumber="ACA123", CarType = CarType.Estate, Status = Status.Unavailable, BaseDayPrice = 670, BaseKmPrice = 7, DistanceMeter = 650 },
            new Car{ Id = 7, RegistrationNumber="BCA123", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 890, BaseKmPrice = 15, DistanceMeter = 500 },
            new Car{ Id = 8, RegistrationNumber="ABC321", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 920, BaseKmPrice = 10, DistanceMeter = 4000 },
            new Car{ Id = 9, RegistrationNumber="BCC133", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 890, BaseKmPrice = 15, DistanceMeter = 500 },
            new Car{ Id = 10, RegistrationNumber="AAC111", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 920, BaseKmPrice = 10, DistanceMeter = 4000 },
            new Car{ Id = 11, RegistrationNumber="CCA123", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 890, BaseKmPrice = 15, DistanceMeter = 500 },
            new Car{ Id = 12, RegistrationNumber="ABC331", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 920, BaseKmPrice = 10, DistanceMeter = 4000 }
        };

        private Customer[] mockCustomers = new Customer[]
        {
            new Customer { Id = 1, IdentityNumber = "111111-1111" },
            new Customer { Id = 2, IdentityNumber = "222222-2222" },
            new Customer { Id = 3, IdentityNumber = "333333-2222" },
            new Customer { Id = 4, IdentityNumber = "444444-2222" },
            new Customer { Id = 5, IdentityNumber = "555555-2222" }
        };

        private Registration[] mockRegistrations = new Registration[]
        {
            new Registration { Id = 1, DateTime = DateTime.Now.AddMonths(-1).AddDays(-15), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
            new Registration { Id = 2, DateTime = DateTime.Now.AddMonths(-1).AddDays(-10), DistanceMeter = 30, RegistrationType = RegistrationType.Return },
            new Registration { Id = 3, DateTime = DateTime.Now.AddMonths(-1).AddDays(-2), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
            new Registration { Id = 4, DateTime = DateTime.Now.AddMonths(-1).AddDays(-1), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
            new Registration { Id = 5, DateTime = DateTime.Now.AddDays(-25), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
            new Registration { Id = 6, DateTime = DateTime.Now.AddDays(-23), DistanceMeter = 70, RegistrationType = RegistrationType.Return },
            new Registration { Id = 7, DateTime = DateTime.Now.AddDays(-8), DistanceMeter = 150, RegistrationType = RegistrationType.PickUp },
            new Registration { Id = 8, DateTime = DateTime.Now.AddDays(-5), DistanceMeter = 100, RegistrationType = RegistrationType.Return }
        };

        private Booking[] mockBookings = new Booking[]
        {
            new Booking { Id = 1, CarId = 1, CustomerId = 1, PickUpRegistrationId = 1, ReturnRegistrationId = 2},
            new Booking { Id = 2, CarId = 3, CustomerId = 2, PickUpRegistrationId = 3 },
            new Booking { Id = 3, CarId = 6, CustomerId = 3, PickUpRegistrationId = 4 },
            new Booking { Id = 4, CarId = 8, CustomerId = 4, PickUpRegistrationId = 5, ReturnRegistrationId = 6},
            new Booking { Id = 5, CarId = 4, CustomerId = 5, PickUpRegistrationId = 7, ReturnRegistrationId = 8}
        };

        #endregion

        public BookingsControllerTests()
        {
            bookingRepository = new Mock<IBookingRepository>();
            customerRepository = new Mock<ICustomerRepository>();
            carRepository = new Mock<ICarRepository>();
            registrationRepository = new Mock<IRegistrationRepository>();

            bookingController = new BookingController(bookingRepository.Object);
            carsController = new CarsController(carRepository.Object);

            bookingRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockBookings.Where(x => x.Id > 0)));
            bookingRepository.Setup(repo => repo.GetById(1)).Returns(Task.FromResult(mockBookings[0]));
            customerRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockCustomers.Where(x => x.Id > 0)));
            carRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockCars.Where(x => x.Id > 0)));
            carRepository.Setup(repo => repo.GetById(1)).Returns(Task.FromResult(mockCars[0]));
            registrationRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockRegistrations.Where(x => x.Id > 0)));
        }

        [Fact]
        public async void ANewCustomerShouldBeAddedToTheSystemThroughAValidBooking()
        {
            var car = await carsController.Index(1);
            var booking = new Booking
            {
                Customer = new Customer() { IdentityNumber = "123456-1234" },
                Car = car,
                PickUpRegistration = new Registration
                {
                    RegistrationType = RegistrationType.PickUp,
                    DateTime = DateTime.Now,
                    DistanceMeter = car.DistanceMeter
                }
            };
            var result = await bookingController.Add(booking);
            Assert.True(result, "A new customer should be added to the system through a valid booking");
        }


        [Fact]
        public async void ReturnRegistrationCanNotHaveDateOlderThanPickUpRegistration()
        {
            var car = await carsController.Index(1);
            var booking = new Booking
            {
                Customer = new Customer() { IdentityNumber = "123454-1234"},
                Car = car,
                PickUpRegistration = new Registration
                {
                    RegistrationType = RegistrationType.PickUp,
                    DateTime = DateTime.Now,
                    DistanceMeter = car.DistanceMeter
                },
                ReturnRegistration = new Registration
                {
                    RegistrationType = RegistrationType.Return,
                    DateTime = DateTime.Now.AddHours(-1),
                    DistanceMeter = car.DistanceMeter
                }
            };
            var result = await bookingController.Update(booking);
            Assert.False(result, "A return registration date can not be older than the pick up registration date");
        }

        [Fact]
        public async void ReturnRegistrationCanNotHaveLessDistanceMeterThanPickUpRegistration()
        {
            var customer = new Customer() { IdentityNumber = "123454-1234" };
            var car = await carsController.Index(1);
            var booking = new Booking
            {
                Customer = customer,
                Car = car,
                PickUpRegistration = new Registration
                {
                    RegistrationType = RegistrationType.PickUp,
                    DateTime = DateTime.Now,
                    DistanceMeter = car.DistanceMeter
                },
                ReturnRegistration = new Registration
                {
                    RegistrationType = RegistrationType.Return,
                    DateTime = DateTime.Now.AddHours(-1),
                    DistanceMeter = car.DistanceMeter - 10
                }
            };

            var result = await bookingController.Update(booking);

            Assert.False(result, "A return registration can not have less distance meter than the pick up registration date");
        }
    }
}
