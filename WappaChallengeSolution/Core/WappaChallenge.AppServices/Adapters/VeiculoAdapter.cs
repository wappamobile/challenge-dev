﻿using WappaChallenge.Dominio.Entidades;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Adapters
{
    public static class VeiculoAdapter
    {
        public static Veiculo ParaObjetoDeDominio(this VeiculoDTO dto)
        {
            return new Veiculo(dto.Marca, dto.Modelo, dto.Placa);
        }

        public static VeiculoDTO ParaDTO(this Veiculo entidade)
        {
            return new VeiculoDTO
            {
                Id = entidade.Id,
                Marca = entidade.Marca,
                Modelo = entidade.Modelo,
                Placa = entidade.Placa
            };
        }
    }
}
