using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Wappa.DataAccess
{
    public static class SeedData
    {

        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            WappaDbContext context = serviceProvider.GetService<WappaDbContext>();
            context.Database.Migrate();
            if (!context.Taxistas.Any())
            {
                var rdn = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var trailling = i.ToString().PadLeft(3, '0');
                    context.Taxistas.Add(new Models.Taxista
                    {
                        Marca = $"Marca de Teste - {trailling}",
                        Modelo = $"Modelo de Teste - {trailling}",
                        PrimeiroNome = $"Taxista de Teste - {trailling}",
                        UltimoNome = $"Último nome do Taxista de Teste - {trailling}",
                        Placa = $"XYZ-0{trailling}",
                        Latitude = (rdn.NextDouble() * rdn.Next(1, 180)).ToString(),
                        Longitude = (rdn.NextDouble() * rdn.Next(1, 180)).ToString(),
                        Endereco = $"Rua de Teste, {i+1}, Jardim Qualquer, SP, Brasil"
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
