using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.Infrastructure.Context;

namespace Wappa.Challenge.Infrastructure.Repositories
{
    public class MotoristaRepository : ABaseRepository<Motorista>, IMotoristaRepository
    {
        public bool Apagar(long id)
        {
            using (var context = new DatabaseContext())
            {
                var model = context.Motorista.Find(id);

                context.Remove(model);
                return (context.SaveChanges() > 0);
            }
        }

        public override Motorista Adicionar(Motorista entity)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    Context.Set<Motorista>().Add(entity);
                    Context.SaveChanges();
                }

                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override IEnumerable<Motorista> Listar()
        {
            using (var context = new DatabaseContext())
            {
                var model = from m in context.Motorista
                            select new Motorista()
                            {
                                IdMotorista = m.IdMotorista,
                                Nome = m.Nome,
                                Sobrenome = m.Sobrenome,
                                Carro = m.Carro,
                                Endereco = m.Endereco
                            };

                return model.ToList();
            }
        }
    }
}
