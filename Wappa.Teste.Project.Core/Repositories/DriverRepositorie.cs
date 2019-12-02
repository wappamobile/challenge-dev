using System.Collections.Generic;
using System.Linq;
using Wappa.Teste.Project.Core.Models;
using Wappa.Teste.Project.Infra.Helper;

namespace Wappa.Teste.Project.Core.Repositories
{
    public class DriverRepositorie
    {
        public static List<DriverModel> drivers = new List<DriverModel>();

        public static void InsertDriver(DriverModel driver)
        {
            driver.Id = drivers.Count > 0 ? drivers.Last().Id + 1 : 1;
            drivers.Add(driver);
        }

        public static void UpdateDriver(DriverModel driver)
        {
            foreach (var item in drivers)
            {
                if (item.Id != driver.Id)
                    continue;

                item.Name = driver.Name;
                item.Surname = driver.Surname;

                if (item.Vehicle != null && item.Vehicle != null)
                {
                    item.Vehicle.Brand = driver.Vehicle.Brand;
                    item.Vehicle.Model = driver.Vehicle.Model;
                    item.Vehicle.Plate = driver.Vehicle.Plate;
                }
                else
                    item.Vehicle = driver.Vehicle;

                if (item.Address != null && driver.Address != null)
                {
                    if (item.Address.ZipCode != driver.Address.ZipCode)
                    {
                        GoogleModelHelper coordinate = GoogleHelper.GetCoordinate(driver.Address.ZipCode.Replace("-", ""));
                        driver.Address.Coordinate = new CoordinateModel
                        {
                            Latitude = coordinate.Results[0].Geometry.Location.Latitude,
                            Longitude = coordinate.Results[0].Geometry.Location.Longitude
                        };
                    }

                    item.Address.Street = driver.Address.Street;
                    item.Address.Number = driver.Address.Number;
                    item.Address.City = driver.Address.City;
                    item.Address.State = driver.Address.State;
                    item.Address.ZipCode = driver.Address.ZipCode;
                }
                else
                    item.Address = driver.Address;


                break;
            }
        }

        public static DriverModel GetDriver(int id)
        {
            return drivers.Where(q => q.Id == id).FirstOrDefault();
        }

        public static List<DriverModel> GetAllDrivers()
        {
            return drivers;
        }

        public static void DeleteDriver(int id)
        {
            drivers.RemoveAll(q => q.Id == id);
        }
    }
}
