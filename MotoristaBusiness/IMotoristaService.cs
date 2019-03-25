using MotoristaEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotoristaBusiness
{
    public interface IMotoristaService
    {
        Motorista AddMotorista(Motorista motorista);
        int UpdateMotorista(Motorista motorista);
        int DeleteMotorista(int motoristaId);
        Motorista GetMotorista(int motoristaId);
        List<Motorista> GetMotorista();

    }
}
