using System.Collections.Generic;
using testewappa.Models;

namespace testewappa.Repository
{
    public interface IWappaRepository
    {
         void Add(Motorista motorista);

        Motorista Find(int id);
        IEnumerable<Motorista> GetAll(string order);

        void Delete(int id);

        void Update(Motorista motorista);

    }
}