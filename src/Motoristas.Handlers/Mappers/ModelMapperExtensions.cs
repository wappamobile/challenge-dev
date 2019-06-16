using AutoMapper;
using AutoMapper.Mappers;
using Motoristas.Core;
using Motoristas.Handlers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Mappers
{
    public static class ModelMapperExtensions
    {
        private static readonly IMapper Mapper;

        static ModelMapperExtensions()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddConditionalObjectMapper().Where((s, d) => s.Name == $"{d.Name}Model");
                c.AddConditionalObjectMapper().Where((s, d) => $"{s.Name}Model" == d.Name);
            });
            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

        public static Motorista ToDomain(this MotoristaModel model)
        {
            return Mapper.Map<Motorista>(model);
        }

        public static MotoristaModel ToModel(this Motorista domain)
        {
            return Mapper.Map<MotoristaModel>(domain);
        }

    }
}
