namespace Wappa.Motoristas.API.Application.DTO
{
	public class CoordenadasDTO
	{
		public CoordenadasDTO(decimal longitude, decimal latitude)
		{
			Longitude = longitude;
			Latitude = latitude;
		}

		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }
	}
}
