using System;

namespace CarRental.Database.Models
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public RegistrationType RegistrationType { get; set; }
        public string DateTime { get; set; }
        public int DistanceMeter { get; set; }

        public RegistrationDto(int id, RegistrationType registrationType, DateTime dateTime, int distanceMeter)
        {
            Id = id;
            RegistrationType = registrationType;
            DateTime = dateTime.ToString("yyyy-MM-dd HH:mm");
            DistanceMeter = distanceMeter;
        }
    }
}
