using System;
using System.Collections.Generic;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Models;

namespace Wappa.Challenge.Domain.Repositories
{
    public interface IDriverRepository
    {
        void Insert(Driver driver);
        void Update(Driver driver);
        void Delete(Guid driverId);
        bool Exists(Guid driverId);
        List<Driver> List(OrderByOptionCommand orderBy);
    }
}