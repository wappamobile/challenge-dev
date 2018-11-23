using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Challenge.Dev.Data;
using Wappa.Challenge.Dev.Models;
using Wappa.Challenge.Dev.Services;

namespace Wappa.Challenge.Dev.Negocio
{
    public class CadastroMotoristas
    {
        private IBaseRepositorio<Motorista> _repositorioMotoristas;
        private IGeoCoordenadaService _geoCoordinateService;

        public CadastroMotoristas(
            IBaseRepositorio<Motorista> repositorioMotoristas,
            IGeoCoordenadaService geoCoordinateService = null)
        {
            _repositorioMotoristas = repositorioMotoristas;
            _geoCoordinateService = geoCoordinateService;
        }

        public Motorista ObterMotorista(int id)
        {
            return _repositorioMotoristas.Obter(id);
        }

        public IEnumerable<Motorista> ListarMotoristas<X>(Func<Motorista, X> keySelector, string direcao = "ASC")
        {
            var motoristas = _repositorioMotoristas.Queryable;
            if (keySelector != null)
            {
                switch (direcao?.ToUpper())
                {
                    case "ASC":
                        motoristas = motoristas.OrderBy(keySelector);
                        break;
                    case "DESC":
                        motoristas = motoristas.OrderByDescending(keySelector);
                        break;
                }
            }

            return motoristas.ToList();
        }

        public bool SalvarMotorista(Motorista motorista)
        {
            if (_geoCoordinateService != null && motorista.Endereco != null)
            {
                var geoCoordenadas = _geoCoordinateService.ObterGeoCoordenada(motorista.Endereco);

                motorista.Endereco.Latitude = geoCoordenadas.Latitude;
                motorista.Endereco.Longitude = geoCoordenadas.Longitude;
            }

            return _repositorioMotoristas.Salvar(motorista) > 0;
        }

        public bool ExcluirMotorista(int id)
        {
            return _repositorioMotoristas.Excluir(id) > 0;
        }
    }
}
