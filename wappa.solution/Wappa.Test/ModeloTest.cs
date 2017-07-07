using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Wappa.Domain.Entities;
using Wappa.Domain.Concrete;
using Wappa.Domain.Abstract;
using System.Collections.Generic;

namespace Wappa.Test
{
    public class ModeloTest
    {
        [Fact]
        public void Test1()
        {
            ModeloRepository x = new ModeloRepository();
            Modelo modelo = new Modelo() { Marca =  new Marca() {ID = 1}, Descricao = "Gol" };
            x.add(modelo);
            int id = modelo.ID;
            Assert.True(id > 0);
        }

        [Fact]
        public void Test2() {
            IModeloRepository x = new ModeloRepository();
            Modelo modelo = x.get(1);
            Assert.Equal("Gol", modelo.Descricao);
        }

        [Fact]
        public void Test3() {
            Marca marca = new Marca() {ID = 1};
            IModeloRepository x = new ModeloRepository();
            List<Modelo> modelos = x.get(marca);
            Assert.True(modelos.Count > 0);
        }        

    }
}