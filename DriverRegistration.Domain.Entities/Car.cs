using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Entities
{
    public class Car: ICar
    {
        #region Properties
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Plate { get; set; }
        #endregion
    }
}
