using System;

namespace Wappa.Motoristas.API.Application.DTO
{
    public class CarroDTO
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }        
    }
}