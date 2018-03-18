using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.DataAccess.Contracts;
using Wappa.Models;

namespace Wappa.Application
{
    public class TaxistaFacade : ITaxistaFacade
    {
        private ITaxistaApp taxistaApp;
        private IGeocodingApp geocodingApp;
        public TaxistaFacade(ITaxistaApp taxistaApp, IGeocodingApp geocodingApp)
        {
            this.taxistaApp = taxistaApp;
            this.geocodingApp = geocodingApp;
        }



        public int Delete(int id)
        {
            return taxistaApp.Delete(id);
        }

        public Taxista Find(int id)
        {
            return taxistaApp.Find(id);
        }

        public int GetTotal()
        {
            return taxistaApp.GetTotal();
        }

        public async Task<int> Save(Taxista taxista)
        {
            if (!taxista.IdTaxista.HasValue)
                return await Insert(taxista);
            return await Update(taxista);
        }
        public async Task<int> Insert(Taxista taxista)
        {
            var geoLocalizacao = await geocodingApp.BuscarCoordenadasGeograficas(taxista.Endereco);
            taxista.Latitude = geoLocalizacao?.Latitude;
            taxista.Longitude = geoLocalizacao?.Longitude;
            return taxistaApp.Insert(taxista);
        }
        public async Task<int> Update(Taxista taxista)
        {
            var geoLocalizacao = await geocodingApp.BuscarCoordenadasGeograficas(taxista.Endereco);
            taxista.Latitude = geoLocalizacao?.Latitude;
            taxista.Longitude = geoLocalizacao?.Longitude;
            return taxistaApp.Update(taxista);
        }

        public List<Taxista> List()
        {
            return taxistaApp.List();
        }

        public List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby)
        {
            return taxistaApp.PagedList(pageSize, pageNumber, order, orderby);
        }

    }
}
