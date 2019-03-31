using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MG.WappaDriverAPI.Core.Data.Interfaces;
using MG.WappaDriverAPI.Core.Data.Repositories;
using MG.WappaDriverAPI.Core.Models;
using MG.WappaDriverAPI.Core.Services;
using MG.WappaDriverAPI.Core.Services.Interfaces;
using MG.WappaDriverAPI.Core.Validators;
using MG.WappaDriverAPI.Test.Data;
using MG.WappaDriverAPI.Test.Mock;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace MG.WappaDriverAPI.Test
{
    public class DriverServiceTest
    {
        private IDriverService _driverService;
        private IDriverRepository _driverRepository;
        private IAddressRepository _addressRepository;
        private DriverValidator _driverValidator;
        private IGoogleApiMapsService _googleApiMapsService;
        private AddressValidator _addressValidator;

        public DriverServiceTest()
        {
            _driverRepository = new DriverRepositoryMock();
            _addressRepository = new AddressRepositoryMock();
            _driverValidator = new DriverValidator();
            _googleApiMapsService = new GoogleApiMapsServiceMock();
            _addressValidator = new AddressValidator();
            _driverService = new DriverService(_driverRepository, _addressRepository, _driverValidator, _googleApiMapsService, _addressValidator);
        }


        [Fact]
        public void TestSaveDriver()
        {
            var driver = new Driver
            {
                Car = new Car()
                {
                    Brand = "Toyota",
                    CarColor = "Prata",
                    CarPlate = "NOC-8081",
                    Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                },
                FirstName = "Marcio",
                LastName = "Borges Alonso Guilherme"
            };

            try
            {
                var drive=_driverService.SaveOrUpdate(driver);
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }

        [Fact]
        public void TestSaveAddress()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Borges Alonso Guilherme"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                var driverAddress = _driverService.SaveDriverAddress(drive.Id.ToString(),"Teste", "Rua Henrique Schaumann, 600");
                Assert.True(driverAddress!=null);
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }



        [Fact]
        public void TestGetAddressByDriverId()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Borges Alonso Guilherme"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                var objs = _driverService.GetAddressesByDriverId(drive.Id.ToString());
                Assert.True(objs != null);
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }


        [Fact]
        public void TestGetAddress()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Borger"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                var driverAddress = _driverService.SaveDriverAddress(drive.Id.ToString(), "Teste", "Rua Vergueiro, 2016 - Vila Mariana");
                var obj = _driverService.GetDriverAddressById(driverAddress.Id.ToString());
                Assert.True(obj != null, obj.ZipOrPostcode);
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }


        [Fact]
        public void TestDriverFindByName()
        {
            try
            {
                var objs = _driverService.FindByName("Marcio");
                Assert.True(objs != null, objs.Count().ToString());
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }


        [Fact]
        public void TestDriverFindByName2()
        {
            try
            {
                var objs = _driverService.FindByName("Marcio","Guilherme");
                Assert.True(objs != null, objs.Count().ToString());
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }


        [Fact]
        public void TestDriverFindByName3()
        {
            try
            {
                var objs = _driverService.FindByName("Abobra", "Guilherme");
                Assert.True(objs != null, objs.Count().ToString());
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }


        [Fact]
        public void TestGetFullDriverById()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Alonso"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                var obj = _driverService.GetFullDriverById(drive.Id.ToString());
                Assert.True(obj != null, obj.FirstName);
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }

        [Fact]
        public void TestGetCarByDriverId()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Alonso"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                var obj = _driverService.GetCarByDriverId(drive.Id.ToString());
                Assert.True(obj != null, obj.CarPlate);
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }

        [Fact]
        public void TestGetDriverById()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Borger"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                 _driverService.SaveDriverAddress(drive.Id.ToString(), "Teste", "Rua Vergueiro, 2016 - Vila Mariana");
                var obj = _driverService.GetDriverById(drive.Id.ToString());
                Assert.True(obj != null && (obj.Addresses==null || !obj.Addresses.Any()), "Ok");
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }


        [Fact]
        public void TestDeleteDriverAddress()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Borger"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                var driverAddress = _driverService.SaveDriverAddress(drive.Id.ToString(), "Teste", "Rua Vergueiro, 2016 - Vila Mariana");
                _driverService.DeleteDriverAddress(driverAddress.Id.ToString());
                Assert.True(true, "Ok");
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }

        [Fact]
        public void TestDeleteDriver()
        {
            try
            {
                var driver = new Driver
                {
                    Car = new Car()
                    {
                        Brand = "Toyota",
                        CarColor = "Prata",
                        CarPlate = "NOC-8081",
                        Model = "Etios Hatch X Man 1.3 L 16 V Dual VVT-i Flex",
                    },
                    FirstName = "Marcio",
                    LastName = "Borger"
                };
                var drive = _driverService.SaveOrUpdate(driver);
                _driverService.DeleteDriver(drive.Id.ToString());
                Assert.True(true, "Ok");
            }
            catch (Exception e)
            {
                Assert.False(true, e.ToString());
            }
        }

    }
}
