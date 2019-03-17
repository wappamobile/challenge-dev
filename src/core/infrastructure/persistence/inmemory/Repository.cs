using System;
using System.Collections.Generic;
using System.Linq;
using WappaMobile.ChallengeDev.Models;

namespace WappaMobile.ChallengeDev.Persistence
{
    abstract class Repository<T> where T : Entidade
    {
        protected readonly List<T> _cache = new List<T>();

        public T PegarPeloId(Guid id)
        {
            return _cache.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Excluir(Guid id)
        {
            var x = PegarPeloId(id);
            _cache.Remove(x);
        }

        public IEnumerable<T> Listar()
        {
            return _cache;
        }

        public void Salvar(T entidade)
        {
            if(_cache.Contains(entidade))
                Atualizar(entidade);
            else
                Incluir(entidade);
        }

        public void Incluir(T entidade)
        {
            _cache.Add(entidade);
        }

        public void Atualizar(T entidade)
        {
            Excluir(entidade.Id);
            Incluir(entidade);
        }
    }
}