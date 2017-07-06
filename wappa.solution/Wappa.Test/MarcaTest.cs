using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Wappa.Domain.Entities;
using Wappa.Domain.Concrete;
using Wappa.Domain.Abstract;

namespace Wappa.Test
{
    public class MarcaTest
    {
        [Fact]
        public void Test1()
        {
            IMarcaRepository x = new MarcaRepository();
            Marca marca = new Marca() { Descricao = "Volkswagen" };
            x.add(marca);
            int id = marca.ID;
            Assert.True(id > 0);
        }

        [Fact]
        public void Test2() {
            IMarcaRepository x = new MarcaRepository();
            Marca marca = x.get(1);
            Assert.Equal("Volkswagen", marca.Descricao);
        }

    }
}