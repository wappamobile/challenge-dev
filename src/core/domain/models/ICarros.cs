using System.Collections.Generic;

namespace WappaMobile.ChallengeDev.Models
{
    public interface ICarros : ILeitura<Carro>, IEscrita<Carro>
    {
        Carro BuscarPelaPlaca(Placa placa);
    }
}
