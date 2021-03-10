using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.DTO
{
	public class MotoristaDTO
	{
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        
        public EnderecoDTO Endereco { get; set; }
        public CarroDTO Carro { get; set; }        

        public static List<MotoristaDTO> ParaMotoristaDTO(dynamic motoristas)
        {
            var listaDeMotoristas = new List<MotoristaDTO>();

            foreach (var motorista in motoristas)
            {
                var motoristaDTO = new MotoristaDTO
                {
                    Id = Guid.Parse(Convert.ToString(motorista.ID)),
                    Nome = motorista.NOME,
                    SobreNome = motorista.SOBRENOME,
                    Carro = new CarroDTO(),
                    Endereco = new EnderecoDTO()
                };

                motoristaDTO.Endereco = new EnderecoDTO
                {
                    Logradouro = motorista.LOGRADOURO,
                    Numero = motorista.NUMERO,
                    Complemento = motorista.COMPLEMENTO,
                    Bairro = motorista.BAIRRO,
                    Cep = motorista.CEP,
                    Cidade = motorista.CIDADE,
                    Estado = motorista.ESTADO,
                    Coordenadas = new CoordenadasDTO(motorista.LONGITUDE, motorista.LATITUDE)
                };

                motoristaDTO.Carro = new CarroDTO
                {
                    Marca = motorista.MARCA,
                    Modelo = motorista.MODELO,
                    Placa = motorista.PLACA
                };

                listaDeMotoristas.Add(motoristaDTO);

            }
            return listaDeMotoristas;
        }
    }
}
