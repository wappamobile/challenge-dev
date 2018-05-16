using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Models.Geolocalizacao;
using Wappa.Models.Motorista;

namespace Wappa.Services.V1 {
    public interface IGeolocalizacaoService {
        Task<Rootobject> ObterGeolocalizacao (MotoristaModel model);
    }
}