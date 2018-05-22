using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Core.Interfaces;
using Wappa.Core.Models;

namespace Wappa.Service.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaRepository _repMotorista;

        private readonly ICoordenadasRepository _repCoordenadas;

        public MotoristaService(IMotoristaRepository repMotorista, ICoordenadasRepository repCoordenadas)
        {
            _repMotorista = repMotorista;
            _repCoordenadas = repCoordenadas;
        }

        public async Task<int> Delete(int id)
        {
            return await _repMotorista.Delete(id);
        }

        public async Task<Motorista> Get(int id)
        {
            return await _repMotorista.Get(id);
        }

        public async Task<IEnumerable<Motorista>> GetAll()
        {
            return await _repMotorista.GetAll(null);
        }

        public async Task<IEnumerable<Motorista>> GetAll(string ordem)
        {
            return await _repMotorista.GetAll(ordem);
        }

        public async Task<Motorista> Save(Motorista motorista)
        {
            var coordenadas = await _repCoordenadas.Get(motorista.Endereco);

            if (coordenadas != null && coordenadas.Status == "OK")
                motorista.Endereco.Coordenadas = coordenadas.Resultados.First();

            return await _repMotorista.Save(motorista);
        }

        public async Task<Motorista> Update(Motorista motorista)
        {
            var coordenadas = await _repCoordenadas.Get(motorista.Endereco);

            if (coordenadas != null && coordenadas.Status == "OK")
                motorista.Endereco.Coordenadas = coordenadas.Resultados.First();

            return await _repMotorista.Update(motorista);
        }
    }
}