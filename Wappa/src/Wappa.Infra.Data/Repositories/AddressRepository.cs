using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;
using Wappa.Domain.Repositories;
using Wappa.Infra.Data.SqlStatements;

namespace Wappa.Infra.Data.Repositories
{
    public class AddressRepository : Repository, IAddressRepository
    {
        public AddressRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> DeleteAsync(IAddress address)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync(
                                AddressStatements.Delete,
                                new
                                {
                                    AnCdDriver = address.DriverId,
                                    AiAddress = address.Id
                                },
                                commandType: CommandType.StoredProcedure);
                return result.Any();
            }
        }

        public async Task<IEnumerable<IAddress>> GetByDriverIdAsync(int driverId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Address>(
                    AddressStatements.Find,
                    new
                    {
                        AnCdDriver = driverId
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IAddress> SaveAsync(IAddress address)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Address>(
                    AddressStatements.Save,
                    new
                    {
                        AiAddress = address.Id,
                        AnCdDriver = address.DriverId,
                        AcPostalCode = address.PostalCode,
                        AcStreetName = address.StreetName,
                        AcNumber = address.Number,
                        AcNeighborhood = address.Neighborhood,
                        AcCity = address.City,
                        AcStateCode = address.StateCode,
                        AcCountry = address.Country,
                        AnLongitude = address.Longitude,
                        AnLatitude = address.Latitude,
                    },
                    commandType: CommandType.StoredProcedure);

                address.Id = result.Id;
                address.DriverId = result.DriverId;

                return address;
            }
        }
    }
}