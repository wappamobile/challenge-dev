using System.Collections.Generic;
using Wappa.Teste.Project.Core.Models;
using Wappa.Teste.Project.Core.Repositories;
using Wappa.Teste.Project.Infra.Helper;

namespace Wappa.Teste.Project.Core.Services
{
    public class DriverService
    {
        public static void InsertDriver(DriverModel driver)
        {
            if (driver.Address != null && !string.IsNullOrEmpty(driver.Address.ZipCode))
            {
                GoogleModelHelper coordinate = GoogleHelper.GetCoordinate(driver.Address.ZipCode.Replace("-", ""));
                driver.Address.Coordinate = new CoordinateModel
                {
                    Latitude = coordinate.Results[0].Geometry.Location.Latitude,
                    Longitude = coordinate.Results[0].Geometry.Location.Longitude
                };

            }

            DriverRepositorie.InsertDriver(driver);
        }
        
        public static void UpdateDriver(DriverModel driver)
        {
            DriverRepositorie.UpdateDriver(driver);
        }

        public static DriverModel GetDriver(int id)
        {
            return DriverRepositorie.GetDriver(id);
        }

        public static List<DriverModel> GetAllDrivers()
        {
            return DriverRepositorie.GetAllDrivers();
        }

        public static void DeleteDriver(int id)
        {
            DriverRepositorie.DeleteDriver(id);
        }
    }
}
