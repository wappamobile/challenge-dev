using System;

namespace WappaMobile.Domain
{
    /// <summary>
    /// A driver.
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Gets or sets the driver identifier.
        /// </summary>
        /// <value>The driver identifier.</value>
        public Guid DriverId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the car.
        /// </summary>
        /// <value>The car.</value>
        public Car Car { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public Address Address { get; set; }
    }
}
