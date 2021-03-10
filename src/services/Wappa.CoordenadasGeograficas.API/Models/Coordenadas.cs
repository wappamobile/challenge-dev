namespace Wappa.CoordenadasGeograficas.API.Models
{
	public class Coordenadas
	{
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }

		public bool EhValido()
		{
			return Longitude != 0 && Latitude != 0;
		}
	}
}
