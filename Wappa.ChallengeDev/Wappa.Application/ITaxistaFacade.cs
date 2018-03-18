using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.Application
{
    public interface ITaxistaFacade
    {

        List<Taxista> List();
        Task<int> Save(Taxista taxista);
        Task<int> Insert(Taxista taxista);
        Task<int> Update(Taxista taxista);
        int Delete(int id);
        Taxista Find(int id);
        List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby);
        int GetTotal();
    }
}
