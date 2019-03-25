namespace WappaMobile.Domain
{
    /// <summary>
    /// An address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the line 1.
        /// </summary>
        /// <value>The line 1.</value>
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the line 2.
        /// </summary>
        /// <value>The line 2.</value>
        public string Line2 { get; set; }

        /// <summary>
        /// Gets or sets the Municipality.
        /// </summary>
        /// <value>The Municipality.</value>
        public string Municipality { get; set; }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        /// <value>The State.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>The coordinates.</value>
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:WappaMobile.Domain.Address"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:WappaMobile.Domain.Address"/>.</returns>
        public override string ToString()
        {
            return $@"{Line1}
{Line2}
{Municipality}, {State}
{ZipCode}";
        }
    }
}
