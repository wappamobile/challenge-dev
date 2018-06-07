using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using WebFC.Wappa.Teste.Base.Core.Extensions;
using WebFC.Wappa.Teste.Base.Core.Repositorios;

namespace WebFc.Wappa.Teste.Base.EF.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T>, IDisposable where T : class
    {

        protected DbContext db;
        protected DbSet<T> items;

        public RepositorioBase(DbContext db)
        {
            this.db = db;
            this.items = db.Set<T>();

        }

        protected void CheckDisposed()
        {
            if (db == null)
            {
                throw new ObjectDisposedException("Repository<T>");
            }
        }

        public IEnumerable<int> GroupBy(Expression<Func<T, bool>> where, Expression<Func<T, int>> groupBy)
        {
            return items.AsQueryable().Where(where).GroupBy(groupBy).ToList().Select(x => x.Key).ToList();
        }


        public bool VerifyExist(Expression<Func<T, bool>> where)
        {
            //Filtro
            return items.Any(where);
        }

        public int Sum(Expression<Func<T, bool>> where, Expression<Func<T, int>> sum)
        {
            return items.Where(where).Select(sum).DefaultIfEmpty(0).Sum();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return items.Where(where).Count();
        }


        public T Get(Expression<Func<T, bool>> where, string orderBy)
        {
            //Filtro
            T r = items.OrderBy(orderBy).FirstOrDefault(where);
            return r;
        }

        T IRepositorioBase<T>.Get(params object[] keys)
        {

            CheckDisposed();
            return items.Find(keys);

        }

        public IEnumerable<T> GetAllFilter(Expression<Func<T, bool>> where, string orderBy)
        {
            //Filtro
            IQueryable<T> r = items.AsNoTracking().Where(where);
            r = r.OrderBy(orderBy);
            return r.ToList();
        }

        public IEnumerable<T> GetAllFilter(T item, Expression<Func<T, bool>> where, string orderBy)
        {
            //Filtro
            IQueryable<T> r = items.Where(where);
            r = r.OrderBy(orderBy);
            return r.ToList();
        }


        public IEnumerable<T> GetAllFilter(ref PageInfo page, T item, Expression<Func<T, bool>> where, string orderBy)
        {

            //Filtro
            IQueryable<T> r = items.Where(where);

            //Quantidade
            page.RecordsTotal = r.Count();
            page.Start = (page.RecordsTotal > page.Length ? page.Start : 0);

            r = r.OrderBy(orderBy).Skip(page.Start).Take(page.Length);

            return r.ToList();
        }
        IQueryable<T> IRepositorioBase<T>.GetAll(T item)
        {

            CheckDisposed();
            return items;

        }



        T IRepositorioBase<T>.Create(T item)
        {
            CheckDisposed();
            var r = items.Create();
            db.SaveChanges();
            return r;
        }

        T IRepositorioBase<T>.Add(T item)
        {

            CheckDisposed();
            var r = items.Add(item);
            db.SaveChanges();
            return r;
        }

        void IRepositorioBase<T>.Remove(T item)
        {
            CheckDisposed();
            db.Entry(item).State = EntityState.Deleted;
            items.Remove(item);
            db.SaveChanges();

        }

        void IRepositorioBase<T>.RemoveByKey(T item)
        {
            CheckDisposed();
            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();

        }

        void IRepositorioBase<T>.RemoveAll(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> r = items.Where(where).ToList();

            foreach (var item in r)
            {
                db.Entry(item).State = EntityState.Deleted;

            }
            db.SaveChanges();
        }


        void IRepositorioBase<T>.Update(T item)
        {
            CheckDisposed();
            var entry = db.Entry(item);
            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                items.Attach(item);
                entry.State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }


        void IRepositorioBase<T>.ExecProcedure(string commandName, int IdCupom, string UserId, float qtdeNumeros, string serieDaSorte, DateTime dataCadastro)
        {

            CheckDisposed();
            var param1 = IdCupom;
            var param2 = UserId;


            try
            {

                List<object> SqlParameterList = new List<object>();
                SqlParameterList.Add(new SqlParameter("IdCupom", IdCupom));
                SqlParameterList.Add(new SqlParameter("UserId", UserId));
                SqlParameterList.Add(new SqlParameter("qtdeNumeros", qtdeNumeros));




                var result = db.Database.ExecuteSqlCommand(commandName, SqlParameterList.ToArray());

                //db.SaveChanges();

                if (result == 4)
                {
                    //   db.Dispose();    

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        void IRepositorioBase<T>.UpdateNotLog(T item)
        {
            CheckDisposed();
            var entry = db.Entry(item);
            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                items.Attach(item);
                entry.State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void Dispose()
        {
            if (db.TryDispose())
            {
                db = null;
                items = null;
            }
        }
        //public void AddLog(ModelBase item, object ObjectReturn, Exception ex)
        //{
        //    LogSistemaRepository.AddLog(db, item, ObjectReturn, ex);
        //}
    }
}
