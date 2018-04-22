using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.API.ViewModel;
using Wappa.Models;

namespace Wappa.API.Map
{
    /// <summary>
    /// Classe de mapeamento
    /// </summary>
    public class WappaMapProfile : Profile
    {
        /// <summary>
        /// Construtor dos mappings
        /// </summary>
        public WappaMapProfile()
        {
            CreateMap<MotoristaViewModel, Motorista>()
                .ReverseMap();

            CreateMap<EnderecoViewModel, Endereco>()
                .ReverseMap();

            CreateMap<CarroViewModel, Carro>()
                .ReverseMap();
        }
    }
}
