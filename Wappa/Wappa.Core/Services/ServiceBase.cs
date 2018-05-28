using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wappa.Core.Repositories;

namespace Wappa.Core.Services
{
    public class ServiceBase<T> : IDisposable, IServiceBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> Add(T obj)
        {
            return await _repository.Add(obj);
        }

        public virtual void Remove(T obj)
        {
            _repository.Remove(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _repository.GetAll(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Exists(string id)
        {
            return await _repository.Exists(id);
        }

        public virtual T Update(T obj)
        {
            return _repository.Update(obj);
        }

    }
}
