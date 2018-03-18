using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Models;

namespace Wappa.DataAccess.Contracts
{
    public interface ITaxistaRepository
    {
        int Delete(int id);
        Taxista Find(int id);
        int GetTotal();
        int Insert(Taxista taxista);
        List<Taxista> List();
        List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby);
        int Update(Taxista taxista);
    }
}
