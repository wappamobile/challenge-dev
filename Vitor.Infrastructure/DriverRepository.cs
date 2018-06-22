using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vitor.Application.Options;
using Vitor.Domain.Messages;
using Vitor.Domain.Messages.Request;
using Vitor.Domain.Messages.Response;
using Vitor.Domain.Model;
using Vitor.Domain.Repository;
using Vitor.Infrastructure.Repositories;

namespace Vitor.Infrastructure
{
    public class DriverRepository : Repository, IDriverRepository
    {
        public DriverRepository(PostgresOptions options) : base(options)
        {
        }

        public async Task<DeleteDriverResponse> DeleteDriver(long id)
        {
            var response = new DeleteDriverResponse();
            try
            {
                var connection = CreateConnection();
                string command = (@"DELETE FROM  motorista.driver 
                                    WHERE id = @id");
                await connection.QueryAsync<Driver>(command, new { id });
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON DELETE DRIVER", ex);
            }
        }

        public async Task<GetDriverResponse> GetDriver(long id)
        {
            var response = new GetDriverResponse();
            try
            {
                var connection = CreateConnection();
                string command = (@"SELECT DRIVER.ID,
                                           FIRST_NAME                AS      FIRSTNAME,
                                           LAST_NAME                 AS      LASTNAME,
                                           LOGIN,
                                           EMAIL,
                                           DRIVER.REGISTRATION_DATA  AS    REGISTRATIONDATA,
                                           STREET                    AS STREET,
                                           STREET_NUMBER             AS NUMBER ,
                                           ADDRESS_LATITUDE          AS LAT,
                                           ADDRESS_LONGITUDE          AS LNG,
                                           VEHICLE_ID                AS VEHICLEID,
                                           VEHICLE.LICENSE_PLATE     AS LICENSEPLATE,
                                           CAR.NAME                  AS CARNAME,
                                           BRANDS.NAME               AS BRANDNAME
                                    FROM
                                        MOTORISTA.DRIVER DRIVER
                                        LEFT JOIN MOTORISTA.VEHICLE VEHICLE ON VEHICLE.ID = DRIVER.VEHICLE_ID
                                        LEFT JOIN MOTORISTA.CARS CAR ON VEHICLE.CAR_ID = CAR.ID
                                        LEFT JOIN MOTORISTA.BRANDS BRANDS ON BRANDS.ID = CAR.BRAND_ID
                                    WHERE
                                        DRIVER.ID = @id");
                response.Driver = connection.Query<Driver, Address, Location, Vehicle, Driver>(command,
                                        (driver, address, location, vehicle) =>
                                        {
                                            driver.Address = address;
                                            driver.Address.Location = location;
                                            driver.Vehicle = vehicle;
                                            return driver;
                                        }, new { id }, null,
                                          splitOn: "Street, Lat, VehicleId"
                                        ).SingleOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON GET DRIVER", ex);
            }
        }

        public async Task<InsertDriverResponse> InsertDriver(InsertDriverRequest driver)
        {
            var response = new InsertDriverResponse();
            try
            {
                var connection = CreateConnection();
                string command = (@"INSERT INTO motorista.driver(FIRST_NAME, LAST_NAME, LOGIN, EMAIL, VEHICLE_ID,
                                                                 REGISTRATION_DATA,STREET,STREET_NUMBER,
                                                                 ADDRESS_LATITUDE,ADDRESS_LONGITUDE)
                                    VALUES (@firstname, 
                                            @lastname, 
                                            @login, 
                                            @email,
                                            @vehicleid,
                                            @registrationdata,
                                            @street,
                                            @streetnumber,
                                            @addresslat,
                                            @addresslng)");
                connection.QueryAsync<Driver>(command, new
                {
                    firstname = driver.Driver.FirstName,
                    lastname = driver.Driver.LastName,
                    login = driver.Driver.Login,
                    email = driver.Driver.Email,
                    vehicleid = driver.Driver.Vehicle.VehicleId,
                    registrationdata = DateTime.Now,
                    street = driver.Driver.Address.Street,
                    streetnumber = driver.Driver.Address.Number,
                    addresslat = driver.Driver.Address.Location.Lat,
                    addresslng = driver.Driver.Address.Location.Lng
                }).Result.FirstOrDefault();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON INSERT DRIVER", ex);
            }
        }

        public async Task InsertVehicle(Vehicle Vehicle)
        {
            var response = new InsertDriverResponse();
            try
            {
                var connection = CreateConnection();
                string command = (@"INSERT INTO motorista.vehicle(ID, CAR_ID, LICENSE_PLATE)
                                    VALUES (@vehicleid, 
                                            @carid, 
                                            @licenseplate 
                                            )");
                connection.QueryAsync<Driver>(command, new
                {
                    vehicleid = Vehicle.VehicleId,
                    carid = Vehicle.CarId,
                    licenseplate = Vehicle.LicensePlate,
                }).Result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON INSERT VEHICLE", ex);
            }
        }

        public async Task<long> GetNewVehicleId()
        {
            try
            {
                var connection = CreateConnection();
                string command = (@"SELECT nextval('motorista.S_vehicle')");
                var result = connection.QueryAsync<GetNewVehicle>(command).Result.SingleOrDefault();

                return result.Nextval;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON GET NEW VEHICLE ID", ex);
            }
        }

        public async Task<UpdateDriverResponse> UpdatetDriver(UpdateDriverRequest driver)
        {
            var response = new UpdateDriverResponse();
            try
            {
                var connection = CreateConnection();
                string command = (@"UPDATE motorista.driver SET (FIRST_NAME, LAST_NAME, LOGIN, EMAIL, VEHICLE_ID, STREET, STREET_NUMBER) = 
                                                                (@firstname, @lastname, @login, @email, @vehicleid, @street, @streetnumber)
                                    WHERE id = @id");
                connection.QueryAsync<Driver>(command, new
                {
                    firstname = driver.Driver.FirstName,
                    lastname = driver.Driver.LastName,
                    login = driver.Driver.Login,
                    email = driver.Driver.Email,
                    vehicleid = driver.Driver.Vehicle.VehicleId,
                    registrationdata = DateTime.Now,
                    id = driver.Driver.Id,
                    street = driver.Driver.Address.Street,
                    streetnumber = driver.Driver.Address.Number
                }).Result.FirstOrDefault();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON UPDATE DRIVER", ex);
            }
        }

        public async Task<GetDriverResponse> Getdriverbyemail(string email)
        {
            var response = new GetDriverResponse();
            try
            {
                var connection = CreateConnection();
                string command = (@"SELECT DRIVER.ID,
                                           FIRST_NAME                AS      FIRSTNAME,
                                           LAST_NAME                 AS      LASTNAME,
                                           LOGIN,
                                           EMAIL,
                                           DRIVER.REGISTRATION_DATA  AS    REGISTRATIONDATA,
                                           STREET                    AS STREET,
                                           STREET_NUMBER             AS NUMBER ,
                                           ADDRESS_LATITUDE          AS LAT,
                                           ADDRESS_LONGITUDE          AS LNG,
                                           VEHICLE_ID                AS VEHICLEID,
                                           VEHICLE.LICENSE_PLATE     AS LICENSEPLATE,
                                           CAR.NAME                  AS CARNAME,
                                           BRANDS.NAME               AS BRANDNAME
                                    FROM
                                        MOTORISTA.DRIVER DRIVER
                                        LEFT JOIN MOTORISTA.VEHICLE VEHICLE ON VEHICLE.ID = DRIVER.VEHICLE_ID
                                        LEFT JOIN MOTORISTA.CARS CAR ON VEHICLE.CAR_ID = CAR.ID
                                        LEFT JOIN MOTORISTA.BRANDS BRANDS ON BRANDS.ID = CAR.BRAND_ID
                                    WHERE
                                        DRIVER.EMAIL = @email");
                response.Driver = connection.Query<Driver, Address, Location, Vehicle, Driver>(command,
                                        (driver, address, location, vehicle) =>
                                        {
                                            driver.Address = address;
                                            driver.Address.Location = location;
                                            driver.Vehicle = vehicle;
                                            return driver;
                                        }, new { email }, null,
                                          splitOn: "Street, Lat, VehicleId"
                                        ).SingleOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ON GET DRIVER", ex);
            }
        }
    }
}
