using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Entities
{
    public class Address: IAddress
    {
        #region Properties
        public int Id { get; set; }

        public string AddressName { get; set; }

        public int Number { get; set; }

        public string Neighborhood { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string City { get; set; }
        #endregion
    }
}
