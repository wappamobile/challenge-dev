using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Models;

namespace Wappa.Application
{
    public interface ITaxistaApp
    {

        List<Taxista> List();
        int Insert(Taxista taxista);
        int Update(Taxista taxista);
        int Delete(int id);
        Taxista Find(int id);
        List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby);
        int GetTotal();
    }
}
