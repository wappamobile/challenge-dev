using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Contracts;
using Wappa.Contracts.Models;

namespace Wappa.DataAccess
{
    public class DriverDB : IDriverDB
    {
        public async Task DeleteDriver(string id)
        {
            await Task.Run(() =>
            {
                try
                {
                    List<Driver> drivers = new List<Driver>();
                    bool found = false;

                    using (StreamReader sr = new StreamReader("Drivers.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            var temp = sr.ReadLine();

                            if (temp == "")
                            {
                                continue;
                            }

                            if (temp.Contains($"\"Id\":\"{id}\""))
                            {
                                found = true;
                                continue;
                            }

                            drivers.Add(JsonConvert.DeserializeObject<Driver>(temp));
                        }
                        if (!found)
                        {
                            throw new Exception("Driver not found");
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("Drivers.txt"))
                    {
                        foreach (var drv in drivers)
                        {
                            sw.WriteLine(JsonConvert.SerializeObject(drv));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public List<Driver> GetDriversOrderByFirstName()
        {
            try
            {
                var drivers = new List<Driver>();

                using (StreamReader sr = new StreamReader("Drivers.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        var temp = sr.ReadLine();

                        if (temp == "")
                        {
                            continue;
                        }

                        drivers.Add(JsonConvert.DeserializeObject<Driver>(temp));
                    }
                }

                return drivers.OrderBy(x => x.FirstName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Driver> GetDriversOrderByLastName()
        {
            try
            {
                var drivers = new List<Driver>();

                using (StreamReader sr = new StreamReader("Drivers.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        var temp = sr.ReadLine();

                        if (temp == "")
                        {
                            continue;
                        }

                        drivers.Add(JsonConvert.DeserializeObject<Driver>(temp));
                    }
                }

                return drivers.OrderBy(x => x.LastName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveDriver(Driver driver)
        {
            try
            {
                await Task.Run(() =>
                {
                    driver.GenerateUID();

                    using (StreamWriter sw = new StreamWriter("Drivers.txt", true))
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(driver));
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateDriver(Driver driver)
        {
            await Task.Run(() =>
            {
                try
                {
                    List<Driver> drivers = new List<Driver>();
                    bool found = false;

                    using (StreamReader sr = new StreamReader("Drivers.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            var temp = sr.ReadLine();

                            if (temp == "")
                            {
                                continue;
                            }

                            if (temp.Contains($"\"Id\":\"{driver.Id}\""))
                            {
                                found = true;
                                drivers.Add(driver);
                                continue;
                            }

                            drivers.Add(JsonConvert.DeserializeObject<Driver>(temp));
                        }
                        if (!found)
                        {
                            throw new Exception("Driver not found");
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("Drivers.txt"))
                    {
                        foreach (var drv in drivers)
                        {
                            sw.WriteLine(JsonConvert.SerializeObject(drv));
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
