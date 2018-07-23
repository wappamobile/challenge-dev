using Challenge.Dev.Context;
using Challenge.Dev.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Challenge.Dev.Repositories.Impl
{
    public class CarRepository :  ICarRepository
    {

        private readonly ChallengeDevDbContext _dbContext;
        public CarRepository(ChallengeDevDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Car entity)
        {
            _dbContext.Cars.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Car entity)
        {
            _dbContext.Cars.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Car Find(long id)
        {
            return _dbContext.Cars.Include("address").Include("car").FirstOrDefault(x => x.Id == id);
        }

        public Car First(Expression<Func<Car, bool>> predicate)
        {
            return _dbContext.Cars.FirstOrDefault(predicate);
        }

        public IEnumerable<Car> Get(Expression<Func<Car, bool>> predicate)
        {
            return _dbContext.Cars.Where(predicate);
        }

        public IEnumerable<Car> GetAll()
        {
            return _dbContext.Cars;
        }

        public IEnumerable<Car> GetAllOrderBy(Func<Car, object> keySelector)
        {
            return _dbContext.Cars.OrderBy(keySelector);
        }

        public IEnumerable<Car> GetAllOrderByDescending(Func<Car, object> keySelector)
        {
            return _dbContext.Cars.OrderByDescending(keySelector);
        }

        public void Update(Car entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IQueryable<Car> Where(Expression<Func<Car, bool>> predicate)
        {
            return _dbContext.Cars.Where(predicate);
        }

    }
}
