using CarRental.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRental.Database
{
    public class DbInitializer
    {
        public static void Initialize(CarRentalContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cars.Any())
            {
                return;   // DB has been seeded
            }

            var cars = new Car[]
            {
                new Car{ RegistrationNumber="ABC123", CarType = CarType.Small, Status = Status.Available, BaseDayPrice = 400, BaseKmPrice = 0, DistanceMeter = 1190 },
                new Car{ RegistrationNumber="CBA323", CarType = CarType.Small, Status = Status.Available, BaseDayPrice = 420, BaseKmPrice = 0, DistanceMeter = 1750 },
                new Car{ RegistrationNumber="AAA313", CarType = CarType.Small, Status = Status.Unavailable, BaseDayPrice = 400, BaseKmPrice = 0, DistanceMeter = 1500 },
                new Car{ RegistrationNumber="BBB123", CarType = CarType.Estate, Status = Status.Available, BaseDayPrice = 480, BaseKmPrice = 6, DistanceMeter = 750 },
                new Car{ RegistrationNumber="CCC123", CarType = CarType.Estate, Status = Status.Available, BaseDayPrice = 370, BaseKmPrice = 8, DistanceMeter = 350 },
                new Car{ RegistrationNumber="ACA123", CarType = CarType.Estate, Status = Status.Unavailable, BaseDayPrice = 670, BaseKmPrice = 7, DistanceMeter = 650 },
                new Car{ RegistrationNumber="BCA123", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 890, BaseKmPrice = 15, DistanceMeter = 500 },
                new Car{ RegistrationNumber="ABC321", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 920, BaseKmPrice = 10, DistanceMeter = 4000 },
                new Car{ RegistrationNumber="BCC133", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 890, BaseKmPrice = 15, DistanceMeter = 500 },
                new Car{ RegistrationNumber="AAC111", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 920, BaseKmPrice = 10, DistanceMeter = 4000 },
                new Car{ RegistrationNumber="CCA123", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 890, BaseKmPrice = 15, DistanceMeter = 500 },
                new Car{ RegistrationNumber="ABC331", CarType = CarType.Truck, Status = Status.Available, BaseDayPrice = 920, BaseKmPrice = 10, DistanceMeter = 4000 }
            };

            foreach(var car in cars)
            {
                context.Cars.Add(car);
            }

            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer { IdentityNumber = "111111-1111" },
                new Customer { IdentityNumber = "222222-2222" },
                new Customer { IdentityNumber = "333333-2222" },
                new Customer { IdentityNumber = "444444-2222" },
                new Customer { IdentityNumber = "555555-2222" }
            };

            foreach(var customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();

            var registrations = new Registration[]
            {
                new Registration { DateTime = DateTime.Now.AddMonths(-1).AddDays(-15), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
                new Registration { DateTime = DateTime.Now.AddMonths(-1).AddDays(-10), DistanceMeter = 30, RegistrationType = RegistrationType.Return },
                new Registration { DateTime = DateTime.Now.AddMonths(-1).AddDays(-2), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
                new Registration { DateTime = DateTime.Now.AddMonths(-1).AddDays(-1), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
                new Registration { DateTime = DateTime.Now.AddDays(-25), DistanceMeter = 100, RegistrationType = RegistrationType.PickUp },
                new Registration { DateTime = DateTime.Now.AddDays(-23), DistanceMeter = 70, RegistrationType = RegistrationType.Return },
                new Registration { DateTime = DateTime.Now.AddDays(-8), DistanceMeter = 150, RegistrationType = RegistrationType.PickUp },
                new Registration { DateTime = DateTime.Now.AddDays(-5), DistanceMeter = 100, RegistrationType = RegistrationType.Return }
            };

            foreach(var registration in registrations)
            {
                context.Registrations.Add(registration);
            }

            context.SaveChanges();

            var bookings = new Booking[]
            {
                new Booking { CarId = 1, CustomerId = 1, PickUpRegistrationId = 1, ReturnRegistrationId = 2},
                new Booking { CarId = 3, CustomerId = 2, PickUpRegistrationId = 3 },
                new Booking { CarId = 6, CustomerId = 3, PickUpRegistrationId = 4 },
                new Booking { CarId = 8, CustomerId = 4, PickUpRegistrationId = 5, ReturnRegistrationId = 6},
                new Booking { CarId = 4, CustomerId = 5, PickUpRegistrationId = 7, ReturnRegistrationId = 8}
            };

            foreach (var booking in bookings)
            {
                context.Bookings.Add(booking);
            }

            context.SaveChanges();
        }
    }
}
