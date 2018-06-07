using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebFC.Wappa.Teste.Base.Core.Models;

namespace WebFC.Wappa.Teste.Base.Core.Interface
{
    public interface IMotoristasServices
    {

        IEnumerable<Motoristas> GetAllFilter(Expression<Func<Motoristas, bool>> where, string orderBy);

        Motoristas Add(Motoristas Motorista);

        Motoristas Remove(Motoristas Motorista);

        Motoristas Update(Motoristas editarMotorista); 
    }
}
