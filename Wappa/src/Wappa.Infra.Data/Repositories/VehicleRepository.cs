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
    public class VehicleRepository : Repository, IVehicleRepository
    {
        public VehicleRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> DeleteAsync(IVehicle vehicle)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync(
                                VehicleStatements.Delete,
                                new
                                {
                                    AnCdDriver = vehicle.DriverId,
                                    AiVehicle = vehicle.Id
                                },
                                commandType: CommandType.StoredProcedure);
                return result.Any();
            }
        }

        public async Task<IEnumerable<IVehicle>> GetByDriverIdAsync(int driverId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Vehicle>(
                    VehicleStatements.Find,
                    new
                    {
                        AnCdDriver = driverId
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IVehicle> SaveAsync(IVehicle vehicle)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Vehicle>(
                    VehicleStatements.Save,
                    new
                    {
                        AnCdDriver = vehicle.DriverId,
                        AiVehicle = vehicle.Id,
                        AcPlate = vehicle.Plate,
                        AcModel = vehicle.Model,
                        AcFabricator = vehicle.Fabricator,
                    },
                    commandType: CommandType.StoredProcedure);

                vehicle.Id = result.Id;
                vehicle.DriverId = result.DriverId;

                return vehicle;
            }
        }
    }
}