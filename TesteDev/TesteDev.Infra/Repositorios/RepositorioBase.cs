using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TesteDev.Infra.Entidades;
using TesteDev.Infra.Repositorios.Interfaces;

namespace TesteDev.Infra.Repositorios
{
    internal abstract class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        internal readonly Contexto _contexto;
        private DbSet<T> _entidades;

        public RepositorioBase(Contexto contexto)
        {
            _contexto = contexto;
        }

        public virtual T Buscar(int id)
        {
            return (from r in this.Entidades
                    where r.DataRemovido == null
                    && r.Id == id
                    select r).FirstOrDefault();
        }

        public virtual T Criar(T entidade)
        {
            this.Entidades.Add(entidade);
            this._contexto.SaveChanges();

            return entidade;
        }

        public virtual T Atualizar(T entidade)
        {
            this._contexto.Entry(entidade).State = EntityState.Modified;
            this._contexto.SaveChanges();

            return entidade;
        }

        public virtual void Remover(int id)
        {
            var entidade = this.Entidades.Find(id);
            entidade.DataRemovido = DateTime.Now;
            entidade.Ativo = false;
            this._contexto.SaveChanges();
        }

        public virtual bool VerificarExistencia(int id)
        {
            return this.Entidades.Any(r => r.Id == id && r.Ativo == true);
        }

        public DbSet<T> Entidades
        {
            get
            {
                if (_entidades == null)
                {
                    _entidades = _contexto.Set<T>();
                }

                return _entidades;
            }
        }
    }
}
