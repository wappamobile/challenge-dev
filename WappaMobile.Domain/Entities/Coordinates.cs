namespace WappaMobile.Domain
{
    /// <summary>
    /// Longitude and Latitude coordinates.
    /// </summary>
    public struct Coordinates
    {
        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WappaMobile.Domain.Coordinates"/> struct.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public Coordinates(double latitude, double longitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
