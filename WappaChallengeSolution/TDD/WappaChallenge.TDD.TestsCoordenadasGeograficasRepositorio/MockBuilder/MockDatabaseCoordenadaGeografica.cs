using Moq;
using System.Collections.Generic;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.TDD.TestsCoordenadasGeograficasRepositorio.MockBuilder
{
    internal class MockDatabaseCoordenadaGeografica
    {
        private readonly Mock<IDatabase<CoordenadaGeografica, int>> database;
        private readonly ICollection<CoordenadaGeografica> coordenadasGeograficas;

        public MockDatabaseCoordenadaGeografica()
        {
            coordenadasGeograficas = new List<CoordenadaGeografica>
            {
                new CoordenadaGeografica(-1F,-2F),
                new CoordenadaGeografica(-1F,-2F),
                new CoordenadaGeografica(-1F,-2F),
                new CoordenadaGeografica(-1F,-2F)
            };

            this.database.Setup(m => m.Cadastrar(It.IsAny<CoordenadaGeografica>()))
                         .Callback<CoordenadaGeografica>(c => this.coordenadasGeograficas.Add(c))
                         .Returns<CoordenadaGeografica>(c => c);
        }
    }
}
