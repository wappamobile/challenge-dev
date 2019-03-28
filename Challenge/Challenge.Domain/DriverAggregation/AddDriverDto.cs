namespace Challenge.Domain.DriverAggregation
{
    public class AddDriverDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Car Car { get; set; }
        public string Address { get; set; }
    }
}