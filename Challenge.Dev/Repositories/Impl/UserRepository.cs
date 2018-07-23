using Challenge.Dev.Context;
using Challenge.Dev.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Challenge.Dev.Helpers;

namespace Challenge.Dev.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly ChallengeDevDbContext _dbContext;
        public UserRepository(ChallengeDevDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);

            if (user.Address != null)
            {
                user.Address.GetGoogleGeoCode();
                _dbContext.Addresses.Add(user.Address);
            }

            if (user.Car != null)
            {
                _dbContext.Cars.Add(user.Car);
            }

            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public User Find(long id)
        {
            return _dbContext.Users.Include("Address").Include("Car").FirstOrDefault(x => x.Id == id);
        }

        public User First(Expression<Func<User, bool>> predicate)
        {
            return _dbContext.Users.FirstOrDefault(predicate);
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> predicate)
        {
            return _dbContext.Users.Where(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public IEnumerable<User> GetUsers(SortingParams sortingParams)
        {
            var query = _dbContext.Users.Include("Address").Include("Car").AsQueryable();

            if (!string.IsNullOrEmpty(sortingParams.SortBy))
                query = query.Sort(sortingParams.SortBy);

            return query;
        }

        public IEnumerable<User> GetAllOrderBy(Func<User, object> keySelector)
        {
            return _dbContext.Users.OrderBy(keySelector);
        }

        public IEnumerable<User> GetAllOrderByDescending(Func<User, object> keySelector)
        {
            return _dbContext.Users.OrderByDescending(keySelector);
        }

        public bool Update(long id, User user)
        {
            var existing = this.Find(id);
            if (existing == null)
                return false;
            
            _dbContext.Addresses.Remove(existing.Address);
            if (user.Address != null)
            {
                user.Address.GetGoogleGeoCode();
                user.Address.IdUser = id;
                _dbContext.Addresses.Add(user.Address);
            }

            _dbContext.Cars.Remove(existing.Car);
            if (user.Car != null)
            {

                user.Car.IdUser = id;
                _dbContext.Cars.Add(user.Car);
            }

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            _dbContext.Entry(existing).State = EntityState.Modified;

            return (_dbContext.SaveChanges() != 0);
        }

        public IQueryable<User> Where(Expression<Func<User, bool>> predicate)
        {
            return _dbContext.Users.Where(predicate);
        }
    }
}
