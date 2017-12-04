using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.WebApi.ViewModels.Response
{
    public abstract class BaseResponse<T>
    {
        public T[] Items { get; set; }
        public int Count { get { return Items.Count(); } }
    }
}
