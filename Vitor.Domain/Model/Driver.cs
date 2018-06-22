using System;

namespace Vitor.Domain.Model
{
    public class Driver
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationData { get; set; }
        public Address Address { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
