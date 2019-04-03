using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Application.ViewModels;

namespace Wappa.Application.Interfaces
{
    public interface IDriverAppService : IDisposable
    {
        Task Register(DriverViewModel customerViewModel);
        IEnumerable<DriverViewModel> GetAll();
        DriverViewModel GetById(Guid id);
        Task Update(DriverViewModel customerViewModel);
        void Remove(Guid id);        
    }
}
