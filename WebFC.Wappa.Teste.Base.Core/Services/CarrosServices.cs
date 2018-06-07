using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFC.Wappa.Teste.Base.Core.Interface;
using WebFC.Wappa.Teste.Base.Core.Repositorios;

namespace WebFC.Wappa.Teste.Base.Core.Services
{
    public class CarrosServices : ICarrosServices
    {

        private readonly ICarrosRepositorio _CarrosRepositorios; 

        public CarrosServices(ICarrosRepositorio CarrosRepositorio)
        {

            _CarrosRepositorios = CarrosRepositorio; 

        }
    }
}
