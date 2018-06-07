using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFC.Wappa.Teste.Base.Core.Extensions
{
    public static class Helpers
    {

        public static bool TryDispose(this IDisposable item)
        {
            if (item == null) return false;
            item.Dispose();
            return true;
        }

    }

}
