using System.Threading.Tasks;
using Wappa.Driver.Api.Data.Context;
using Wappa.Driver.Api.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Wappa.Driver.Api.Dtos;

namespace Wappa.Driver.Api.Repositories.Implementations
{
    /// <summary>
    /// Implementação da interface IDriverRepository
    /// </summary>
    public class DriverRepository : IDriverRepository<DriverDto>
    {
        #region Private fields
        private readonly DriverContext _context;
        #endregion

        #region Constructor
        public DriverRepository(DriverContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Exclui um objeto
        /// </summary>
        /// <param name="id">Id do objeto a ser excluído</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //Remove o carro do motorista
                    var driverCarEntity = _context.DriverCars.FirstOrDefault(item => item.DriverId == id);
                    _context.DriverCars.Remove(driverCarEntity);

                    //Insere o endereço do motorista
                    var driverAddressEntity = _context.DriverAddresses.FirstOrDefault(item => item.DriverId == id);
                    _context.DriverAddresses.Remove(driverAddressEntity);

                    //Remove o motorista
                    var driverEntity = _context.Drivers.FirstOrDefault(item => item.DriverID == id);
                    _context.Drivers.Remove(driverEntity);

                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        /// <summary>
        /// Verifica se o motorista existe
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns></returns>
        public async Task<bool> Exists(int id)
        {
            var driverEntity = _context.Drivers.FirstOrDefault(item => item.DriverID == id);
            if (driverEntity == null)
                return false;
            return true;
        }

        /// <summary>
        /// Retorna a lista de motoristas
        /// </summary>
        /// <returns></returns>
        public async Task<List<DriverDto>> GetDrivers()
        {
            var result = await(from d in _context.Drivers
                               join c in _context.DriverCars on d.DriverID equals c.DriverId
                               join a in _context.DriverAddresses on d.DriverID equals a.DriverId
                               orderby(d.FirstName)
                               select new DriverDto
                               {
                                   DriverId = d.DriverID,
                                   FirstName = d.FirstName,
                                   LastName = d.LastName,
                                   DriverCarDto = new DriverCarDto
                                   {
                                       CarId = c.CarId,
                                       DriverId = c.DriverId,
                                       Make = c.Make,
                                       Model = c.Model,
                                       Plate = c.Plate
                                   },
                                   DriverAddressDto = new DriverAddressDto
                                   {
                                       Address = a.Address,
                                       AddressId = a.AddressId,
                                       City = a.City,
                                       Country = a.Country,
                                       DriverId = a.DriverId,
                                       Latitude = a.Latitude,
                                       Longitude = a.Longitude,
                                       Neighborhood = a.Neighborhood,
                                       Number = a.Number,
                                       State = a.State
                                   }
                               }
                                ).ToListAsync();
            return result;
        }

        /// <summary>
        /// Insere um objeto
        /// </summary>
        /// <param name="obj">Objeto a se incluído</param>
        /// <returns></returns>
        public async Task<int> Insert(DriverDto obj)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //Insere o motorista
                    var newDriver = await _context.Drivers.AddAsync(new Data.Models.Driver
                    {
                        FirstName = obj.FirstName,
                        LastName = obj.LastName
                    });

                    //Insere o carro do motorista
                    await _context.DriverCars.AddAsync(new Data.Models.DriverCar
                    {
                        DriverId = newDriver.Entity.DriverID,
                        Make = obj.DriverCarDto.Make,
                        Model = obj.DriverCarDto.Model,
                        Plate = obj.DriverCarDto.Plate
                    });

                    //Insere o endereço do motorista
                    await _context.DriverAddresses.AddAsync(new Data.Models.DriverAddress
                    {
                        DriverId = newDriver.Entity.DriverID,
                        Address = obj.DriverAddressDto.Address,
                        City = obj.DriverAddressDto.City,
                        Country = obj.DriverAddressDto.Country,
                        Latitude = obj.DriverAddressDto.Latitude,
                        Longitude = obj.DriverAddressDto.Longitude,
                        Neighborhood = obj.DriverAddressDto.Neighborhood,
                        Number = obj.DriverAddressDto.Number,
                        State = obj.DriverAddressDto.State
                    });

                    _context.SaveChanges();
                    dbContextTransaction.Commit();

                    return newDriver.Entity.DriverID;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
                return 0;
            }
            
        }

        /// <summary>
        /// Atualiza um objeto
        /// </summary>
        /// <param name="obj">Objeto a ser atualizado</param>
        /// <returns></returns>
        public async Task<DriverDto> Update(DriverDto obj)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //Atualiza o motorista
                    var driverEntity = _context.Drivers.FirstOrDefault(item => item.DriverID == obj.DriverId);
                    driverEntity.FirstName = obj.FirstName;
                    driverEntity.LastName = obj.LastName;
                    _context.Drivers.Update(driverEntity);

                    //Atualiza o carro do motorista
                    var driverCarEntity = _context.DriverCars.FirstOrDefault(item => item.DriverId == obj.DriverId);
                    driverCarEntity.Make = obj.DriverCarDto.Make;
                    driverCarEntity.Model = obj.DriverCarDto.Model;
                    driverCarEntity.Plate = obj.DriverCarDto.Plate;
                    _context.DriverCars.Update(driverCarEntity);

                    //Atualiza o endereço do motorista
                    var driverAddressEntity = _context.DriverAddresses.FirstOrDefault(item => item.DriverId == obj.DriverId);
                    driverAddressEntity.Address = obj.DriverAddressDto.Address;
                    driverAddressEntity.City = obj.DriverAddressDto.City;
                    driverAddressEntity.Country = obj.DriverAddressDto.Country;
                    driverAddressEntity.Latitude = obj.DriverAddressDto.Latitude;
                    driverAddressEntity.Longitude = obj.DriverAddressDto.Longitude;
                    driverAddressEntity.Neighborhood = obj.DriverAddressDto.Neighborhood;
                    driverAddressEntity.Number = obj.DriverAddressDto.Number;
                    driverAddressEntity.State = obj.DriverAddressDto.State;

                    _context.DriverAddresses.Update(driverAddressEntity);
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                    return obj;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }

        #endregion
    }
}
