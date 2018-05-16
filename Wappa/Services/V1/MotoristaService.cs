using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Models.Motorista;

namespace Wappa.Services.V1 {
    public class MotoristaService : IMotoristaService {
        public void GravarMotorista (MotoristaModel model) {
            // Gravar resultados - Mockado
        }

        public List<MotoristaResult> ObterMotoristas (bool ordenarPorSobrenome) {

            var listaMockada = new List<MotoristaResult> ();

            listaMockada.Add (new MotoristaResult () {
                PrimeiroNome = "Lucas",
                    UltimoNome = "Rossini",
                    Marca = "Ferrari",
                    Modelo = "Teste",
                    Placa = "ASC-4354",
                    Rua = "Rua teste",
                    Numero = 3,
                    Bairro = "TESSSTE",
                    Cidade = "SANTO TESTE",
                    Cep = 34523452,
                    Pais = "Brasil",
                    Geolocalizacao = new GeolocalizacaoResult () {
                        Latitude = 13453242,
                            Longitude = -3242342
                    }
            });

            listaMockada.Add (new MotoristaResult () {
                PrimeiroNome = "Fulano",
                    UltimoNome = "Da silva",
                    Marca = "VOLKS",
                    Modelo = "FUSCA",
                    Placa = "FUS-1234",
                    Rua = "RUA FUSCA",
                    Numero = 3,
                    Bairro = "FUSCAO",
                    Cidade = "SAO FUSCA",
                    Cep = 34523452,
                    Pais = "Brasil",
                    Geolocalizacao = new GeolocalizacaoResult () {
                        Latitude = 43543,
                            Longitude = -343
                    }
            });

            listaMockada.Add (new MotoristaResult () {
                PrimeiroNome = "Afulano",
                    UltimoNome = "Teste",
                    Marca = "VOLKS",
                    Modelo = "GOLF",
                    Placa = "GOL-1234",
                    Rua = "RUA AAAAAAA",
                    Numero = 3,
                    Bairro = "CENTRO",
                    Cidade = "SAO SADASDASDA",
                    Cep = 34523452,
                    Pais = "BR",
                    Geolocalizacao = new GeolocalizacaoResult () {
                        Latitude = 4353,
                            Longitude = -454544
                    }
            });

            // Feito via linq devido aos dados mockados
            if (!ordenarPorSobrenome)
                listaMockada = listaMockada.OrderBy (x => x.PrimeiroNome).ToList ();
            else
                listaMockada = listaMockada.OrderBy (x => x.UltimoNome).ToList ();

            return listaMockada;
        }

        public MotoristaResult ObterMotorista (string PrimeiroNome, string UltimoNome) {

            return null;
        }
    }
}