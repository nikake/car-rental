namespace CarRental.Database.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int PickUpRegistrationId { get; set; }
        public int? ReturnRegistrationId { get; set; }

        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Registration PickUpRegistration { get; set; }
        public Registration ReturnRegistration { get; set; }
    }
}
