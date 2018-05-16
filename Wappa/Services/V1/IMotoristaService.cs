using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Models.Motorista;

namespace Wappa.Services.V1 {
    public interface IMotoristaService {
        Task GravarMotorista (MotoristaModel model);
        List<MotoristaResult> ObterMotoristas (bool ordenarPorSobrenome);
        MotoristaResult ObterMotorista (string PrimeiroNome, string UltimoNome);
    }
}