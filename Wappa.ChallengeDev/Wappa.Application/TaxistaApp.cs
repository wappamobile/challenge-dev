using System;
using System.Collections.Generic;
using Wappa.DataAccess.Contracts;
using Wappa.Models;

namespace Wappa.Application
{
    public class TaxistaApp : ITaxistaApp
    {
        private ITaxistaRepository repository;
        public TaxistaApp(ITaxistaRepository repository)
        {
            this.repository = repository;
        }

        public int Delete(int id)
        {
            return repository.Delete(id);
        }

        public Taxista Find(int id)
        {
            return repository.Find(id);
        }

        public int GetTotal()
        {
            return repository.GetTotal();
        }

        public int Insert(Taxista taxista)
        {
            return repository.Insert(taxista);
        }

        public List<Taxista> List()
        {
            return repository.List();
        }

        public List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby)
        {
            return repository.PagedList(pageSize, pageNumber, order,orderby);
        }

        public int Update(Taxista taxista)
        {
            return repository.Update(taxista);
        }
    }
}
