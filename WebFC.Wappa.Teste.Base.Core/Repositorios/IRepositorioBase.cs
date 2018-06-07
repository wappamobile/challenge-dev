using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebFC.Wappa.Teste.Base.Core.Extensions;

namespace WebFC.Wappa.Teste.Base.Core.Repositorios
{
    public interface IRepositorioBase<T> : IDisposable where T : class
    {
        T Get(Expression<Func<T, bool>> where, string orderBy);
        IEnumerable<int> GroupBy(Expression<Func<T, bool>> where, Expression<Func<T, int>> groupBy);
        bool VerifyExist(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAllFilter(Expression<Func<T, bool>> where, string orderBy);

        IEnumerable<T> GetAllFilter(T item, Expression<Func<T, bool>> where, string orderBy);

        IEnumerable<T> GetAllFilter(ref PageInfo page, T item, Expression<Func<T, bool>> where, string orderBy);

        IQueryable<T> GetAll(T item);
        T Get(params object[] keys);
        T Create(T item);
        T Add(T item);
        void Remove(T item);
        void RemoveByKey(T item);
        void RemoveAll(Expression<Func<T, bool>> where);
        void Update(T item);
        void UpdateNotLog(T item);
        //  void AddLog(ModelBase item, object ObjectReturn, Exception ex);
        int Sum(Expression<Func<T, bool>> where, Expression<Func<T, int>> sum);

        int Count(Expression<Func<T, bool>> where);

        void ExecProcedure(string commandName, int IdCupom, string UserId, float qtdeNumeros, string serieDaSorte, DateTime dataCadastro);

    }
}
