using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Models
{
    public class TaxistaListViewModel
    {
        public List<Taxista> Taxistas
        {
            get;
            set;
        }

        public PagingInfo PagingInfo
        {
            get;
            set;
        }
    }
}
