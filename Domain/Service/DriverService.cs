using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Domain.Enumerator;
using Domain.Model;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Service
{
    public class DriverService : IDriverService
    {
        readonly List<string> SkipProperties = new List<string>(){
                "CarId",
                "AddressId",
                "Car",
                "Address",
                "Latitude",
                "Longitude"
            };

        public ICollection<Driver> List(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, DriverNameOrdenation order)
        {
            var query = order == DriverNameOrdenation.FirstName ?
                                                     (from d in context.Drivers.Include("Car").Include("Address") orderby d.FirstName select d) :
                                                     (from d in context.Drivers.Include("Car").Include("Address") orderby d.LastName select d);
            var driversList = query.ToList();
            loggerFactory.CreateLogger("DriverService").LogInformation("Drivers retrieved");
            return driversList;
        }

        public string Save(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, IHttpClientFactory clientFactory, IGeocodingRepository geocodingRepository, IGeocodingService geocodingService, string geocodingApiKey, Driver driver)
        {
            try
            {
                driver.Address = geocodingService.GeocodingByAddress(clientFactory, geocodingRepository, geocodingApiKey, driver.Address);
                ModelValidation(driver);
                var successMessage = "Driver persisted";
                context.Drivers.Add(driver);
                context.SaveChanges();
                loggerFactory.CreateLogger("DriverService").LogInformation(successMessage);
                return successMessage;
            }
            catch (Exception e)
            {
                loggerFactory.CreateLogger("DriverService").LogError(e.Message);
                return e.Message;
            }
        }

        public string Update(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, IHttpClientFactory clientFactory, IGeocodingRepository geocodingRepository, IGeocodingService geocodingService, string geocodingApiKey, Driver driver)
        {
            try
            {
                if (driver.Id <= 0)
                    throw new ArgumentException("The id paramater value is invalid");

                ModelValidation(driver);
                var sucessMessage = "Driver updated";

                var driverAux = GetDriverById(context, driver.Id);

                if (driverAux == null)
                    throw new KeyNotFoundException("There are no drivers with the informed ID");

                var addressAux = new Address();

                var equalAddresses = CompareTwoAddresses(driver.Address, driverAux.Address);

                if (!equalAddresses)
                    addressAux = geocodingService.GeocodingByAddress(clientFactory, geocodingRepository, geocodingApiKey, driver.Address);

                var driverFound = BuildObject(context, driver);

                if (!equalAddresses)
                {
                    driverFound.Address.Latitude = addressAux.Latitude;
                    driverFound.Address.Longitude = addressAux.Longitude;
                }

                context.SaveChanges();
                loggerFactory.CreateLogger("DriverService").LogInformation(sucessMessage);
                return sucessMessage;
            }
            catch (Exception e)
            {
                loggerFactory.CreateLogger("DriverService").LogError(e.Message);
                return e.Message;
            }
        }

        public string Delete(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("The id paramater value is invalid");

                var driver = GetDriverById(context, id);

                if (driver == null)
                    throw new KeyNotFoundException("There are no drivers with the informed ID");

                var sucessMessage = "Driver deleted";
                context.Drivers.Remove(driver);
                context.SaveChanges();
                loggerFactory.CreateLogger("DriverService").LogInformation(sucessMessage);
                return sucessMessage;
            }
            catch (Exception e)
            {
                loggerFactory.CreateLogger("DriverService").LogError(e.Message);
                return e.Message;
            }
        }

        Driver GetDriverById(ChallengeDevEntityContext context, int id)
        {
            return (from d in context.Drivers.Include("Car").Include("Address") where d.Id == id select d).FirstOrDefault();
        }

        Driver BuildObject(ChallengeDevEntityContext context, Driver driver)
        {
            var driverFound = GetDriverById(context, driver.Id);

            if (driver == null)
                throw new KeyNotFoundException("There are no drivers with the informed ID");

            var addressFound = driverFound.Address;
            var carFound = driverFound.Car;

            var driverType = driverFound.GetType();
            var addressType = addressFound.GetType();
            var carType = carFound.GetType();

            foreach (var property in driverType.GetProperties())
                if (!SkipProperties.Contains(property.Name))
                    driverType.GetProperty(property.Name).SetValue(driverFound, driverType.GetProperty(property.Name).GetValue(driver));

            foreach (var property in addressType.GetProperties())
                if (!SkipProperties.Contains(property.Name))
                    addressType.GetProperty(property.Name).SetValue(addressFound, addressType.GetProperty(property.Name).GetValue(driver.Address));

            foreach (var property in carType.GetProperties())
                if (!SkipProperties.Contains(property.Name))
                    carType.GetProperty(property.Name).SetValue(carFound, carType.GetProperty(property.Name).GetValue(driver.Car));

            return driverFound;
        }

        void ModelValidation(Driver model)
        {
            if (String.IsNullOrWhiteSpace(model.FirstName) || model.FirstName.Length < 2 || model.FirstName.Length > 50)
                throw new ArgumentException("The firstName paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.LastName) || model.LastName.Length < 2 || model.LastName.Length > 50)
                throw new ArgumentException("The lastName paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Car.Brand) || model.Car.Brand.Length < 3 || model.Car.Brand.Length > 50)
                throw new ArgumentException("The car's brand paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Car.Model) || model.Car.Model.Length < 2 || model.Car.Model.Length > 50)
                throw new ArgumentException("The car's model paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Car.Plate) || model.Car.Plate.Length != 8)
                throw new ArgumentException("The car's plate paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Address.Country) || model.Address.Country.Length < 3 || model.Address.Country.Length > 150)
                throw new ArgumentException("The address' country paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Address.Street) || model.Address.Street.Length < 5 || model.Address.Street.Length > 255)
                throw new ArgumentException("The address' street paramater value is invalid");
            if (model.Address.Number <= 0)
                throw new ArgumentException("The address' number paramater value is invalid");
            if (!String.IsNullOrWhiteSpace(model.Address.Complement) && (model.Address.Complement.Length < 3 || model.Address.Complement.Length > 50))
                throw new ArgumentException("The address' complement paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Address.Neighborhood) || model.Address.Neighborhood.Length < 3 || model.Address.Neighborhood.Length > 50)
                throw new ArgumentException("The address' neighborhood paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Address.City) || model.Address.City.Length < 3 || model.Address.City.Length > 50)
                throw new ArgumentException("The address' city paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Address.State) || model.Address.State.Length < 2 || model.Address.State.Length > 50)
                throw new ArgumentException("The address' state paramater value is invalid");
            if (String.IsNullOrWhiteSpace(model.Address.ZipCode) || model.Address.ZipCode.Length != 9)
                throw new ArgumentException("The address' zip code paramater value is invalid");

            var platePattern = "[A-Z]{3}\\-[0-9]{4}";
            var zipCodePattern = "[0-9]{5}\\-[0-9]{3}";

            var plateMatches = Regex.Matches(model.Car.Plate, platePattern, RegexOptions.IgnoreCase);
            var zipCodeMatches = Regex.Matches(model.Address.ZipCode, zipCodePattern, RegexOptions.IgnoreCase);

            if (plateMatches == null || plateMatches.Count == 0)
                throw new ArgumentException("The car's plate paramater value is invalid");

            if (zipCodeMatches == null || zipCodeMatches.Count == 0)
                throw new ArgumentException("The address' zip code paramater value is invalid");
        }

        bool CompareTwoAddresses(Address address1, Address address2)
        {
            if (address1.Street.ToUpper().Equals(address2.Street.ToUpper()) &&
               address1.Country.ToUpper().Equals(address2.Country.ToUpper()) &&
               address1.Number == address2.Number &&
               address1.Neighborhood.ToUpper().Equals(address2.Neighborhood.ToUpper()) &&
               address1.City.ToUpper().Equals(address2.City.ToUpper()) &&
               address1.State.ToUpper().Equals(address2.State.ToUpper()) &&
               address1.ZipCode.ToUpper().Equals(address2.ZipCode.ToUpper()))
                return true;

            return false;
        }
    }
}
