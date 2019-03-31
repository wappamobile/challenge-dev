using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MG.WappaDriverAPI.Core.Models;
using MongoDB.Bson;

namespace MG.WappaDriverAPI.Core.Validators
{
    public class DriverValidator : AbstractValidator<Driver>
    {
        public DriverValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(3, 50);
            RuleFor(x => x.LastName).NotEmpty().Length(3, 50);
            RuleFor(x => x.Car).NotNull();
            RuleFor(x => x.Car.Brand).NotEmpty();
            RuleFor(x => x.Car.CarPlate).NotEmpty();
            RuleFor(x => x.Car.Model).NotEmpty();
        }

        private bool ValidObjectId(ObjectId id)
        {
            return id!=new ObjectId();
        }

        public bool ValidObjectId(string id)
        {
            var objid = new ObjectId();
            if (ObjectId.TryParse(id, out objid))
            {
                return ValidObjectId(objid);
            }

            return false;
        }
    }
}
