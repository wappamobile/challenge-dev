using WappaMobile.ChallengeDev.Models;

namespace WappaMobile.ChallengeDev.Persistence
{
    public static class Database
    {
        public static IMotoristas Motoristas = new Motoristas();
        public static ICarros Carros = new Carros();
    }
}
