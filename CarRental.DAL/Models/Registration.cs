using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Database.Models
{
    public enum RegistrationType
    {
        PickUp,
        Return
    }

    public class Registration
    {
        public int Id { get; set; }
        public RegistrationType RegistrationType { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public int DistanceMeter { get; set; }
    }
}