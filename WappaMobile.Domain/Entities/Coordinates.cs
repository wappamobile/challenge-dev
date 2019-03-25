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
        public float Longitude { get; }

        /// <summary>
        /// Gets the latidude.
        /// </summary>
        /// <value>The latidude.</value>
        public float Latidude { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WappaMobile.Domain.Coordinates"/> struct.
        /// </summary>
        /// <param name="longitude">Longitude.</param>
        /// <param name="latidude">Latidude.</param>
        public Coordinates(float longitude, float latidude)
        {
            Longitude = longitude;
            Latidude = latidude;
        }
    }
}
