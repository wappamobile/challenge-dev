using System;
using System.Collections.Generic;
using System.Text;
using Wappa.DataAccess.Contracts;
using Wappa.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Wappa.DataAccess
{
    public class EFTaxistaRepository : ITaxistaRepository
    {
        private WappaDbContext context;
        public EFTaxistaRepository(WappaDbContext ctx)
        {
            context = ctx;
        }
        public int Delete(int id)
        {
            var dbEntry = context.Taxistas.FirstOrDefault(p => p.IdTaxista == id);
            if (dbEntry != null)
            {
                context.Taxistas.Remove(dbEntry);
                context.SaveChanges();
            }
            return 1;
        }

        public Taxista Find(int id)
        {
            return context.Taxistas.FirstOrDefault(p => p.IdTaxista == id);
        }

        public int Insert(Taxista taxista)
        {
            context.Taxistas.Add(taxista);
            context.SaveChanges();
            return taxista.IdTaxista > 0 ? 1 : 0;
        }

        public List<Taxista> List()
        {
            return context.Taxistas.ToList();
        }

        public int Update(Taxista taxista)
        {
            var dbEntry = context.Taxistas.FirstOrDefault(p => p.IdTaxista == taxista.IdTaxista);
            if (dbEntry != null)
            {
                dbEntry.PrimeiroNome = taxista.PrimeiroNome;
                dbEntry.UltimoNome = taxista.UltimoNome;
                dbEntry.Marca = taxista.Marca;
                dbEntry.Modelo = taxista.Modelo;
                dbEntry.Placa = taxista.Placa;
                dbEntry.Latitude = taxista.Latitude;
                dbEntry.Longitude = taxista.Longitude;
                dbEntry.Endereco = taxista.Endereco;
                context.SaveChanges();
                return 1;
            }
            return 0;
        }
        public List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby)
        {
            if (order == 1 && orderby == 1)
                return context.Taxistas
                        .OrderByDescending(p => p.UltimoNome)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            else if (order == 1 && orderby == 0)
                return context.Taxistas
                        .OrderByDescending(p => p.PrimeiroNome)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            else if (order == 0 && orderby == 1)
                return context.Taxistas
                        .OrderBy(p => p.UltimoNome)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
            else
                return context.Taxistas
                        .OrderBy(p => p.PrimeiroNome)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

        }
        public int GetTotal()
        {
            return context.Taxistas.Count();
        }
    }
}
