using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.Infrastructure.Context;

namespace Wappa.Challenge.Infrastructure.Repositories
{
    public class CarroRepository : ABaseRepository<Carro>, ICarroRepository
    {
        public bool Apagar(long id)
        {
            using (var context = new DatabaseContext())
            {
                var model = context.Carro.Find(id);

                context.Remove(model);
                return (context.SaveChanges() > 0);
            }
        }
    }
}
