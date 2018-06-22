using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;
using Vitor.Application.Options;
using Vitor.Domain.Messages;
using Vitor.Domain.Messages.Request;
using Vitor.Domain.Messages.Response;
using Vitor.Domain.Model;
using Vitor.Domain.Repository;
using Vitor.Domain.Service;

namespace Vitor.Application
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly GoogleMapsOptions _options;

        public DriverService(IDriverRepository driverRepository, GoogleMapsOptions options)
        {
            _driverRepository = driverRepository;
            this._options = options;
        }

        public Task<DeleteDriverResponse> DeleteDriver(long id)
        {
            try
            {
                return _driverRepository.DeleteDriver(id);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR TRYING TO DELETE DRIVER", ex);
            }
        }

        public Task<GetDriverResponse> GetDriver(long id)
        {
            try
            {
                return _driverRepository.GetDriver(id);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR TRYING TO GET DRIVER", ex);
            }
        }

        public async Task<InsertDriverResponse> InsertDriver(InsertDriverRequest driver)
        {
            try
            {
                driver = await GetGeoCode(driver);
                driver.Driver.Vehicle.VehicleId = await _driverRepository.GetNewVehicleId();
                await _driverRepository.InsertVehicle(driver.Driver.Vehicle);
                return _driverRepository.InsertDriver(driver).Result;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR TRYING TO INSERT DRIVER", ex);
            }
        }

        private async Task<InsertDriverRequest> GetGeoCode(InsertDriverRequest driver)
        {
            var response = await this._options.Url
                    .SetQueryParams(new
                    {
                        address = string.Format("{0},{1}", driver.Driver.Address.Street, driver.Driver.Address.Number),
                        key = this._options.Key
                    })
                    .GetJsonAsync<RootObject>();

            driver.Driver.Address.Location = response.Results[0].Geometry.Location;
            return driver;
        }

        public Task<UpdateDriverResponse> UpdateDriver(UpdateDriverRequest driver)
        {
            try
            {
                return _driverRepository.UpdatetDriver(driver);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR TRYING TO UPDATE DRIVER", ex);
            }
        }

        public Task<GetDriverResponse> Getdriverbyemail(string email)
        {
            try
            {
                return _driverRepository.Getdriverbyemail(email);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR TRYING TO GET DRIVER", ex);
            }
        }
    }
}
