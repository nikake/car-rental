namespace CarRental.Database.Models
{
    public enum CarType
    {
        Small,
        Estate,
        Truck
    }

    public enum Status
    {
        Available,
        Unavailable
    }


    public class Car
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public CarType CarType { get; set; }
        public Status Status { get; set; }
        public int BaseDayPrice { get; set; }
        public int BaseKmPrice { get; set; }
        public int DistanceMeter { get; set; }
    }
}
