using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Models
{
    public class PagingInfo
    {
        public int TotalItems
        {
            get;
            set;
        }

        public int ItemsPerPage
        {
            get;
            set;
        }

        public int CurrentPage
        {
            get;
            set;
        }

        public int Order
        {
            get;
            set;
        }

        public int OrderBy
        {
            get;
            set;
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)this.TotalItems / (decimal)this.ItemsPerPage);
            }
        }
    }
}
