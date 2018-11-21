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
    public class DriverRepository : Repository, IDriverRepository
    {
        public DriverRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> DeleteAsync(IDriver driver)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync(
                                DriverStatements.Delete,
                                new
                                {
                                    AnCdDriver = driver.Id
                                },
                                commandType: CommandType.StoredProcedure);
                return result.Any();
            }
        }

        public async Task<IDriver> GetByDocumentAsync(string document)
        {
            var drivers = await GetSearchAsync(new Driver { Document = document });

            return drivers.FirstOrDefault();
        }

        public async Task<IDriver> GetByIdAsync(int? id)
        {
            var drivers = await GetSearchAsync(new Driver { Id = id });

            return drivers.FirstOrDefault();
        }

        public async Task<IEnumerable<IDriver>> GetSearchAsync(IDriver driver)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Driver>(
                    DriverStatements.Find,
                    new
                    {
                        AnCdDriver = driver.Id,
                        AcDocument = driver.Document,
                        AcFirstName = driver.FirstName,
                        AcLastName = driver.LastName
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> HasDriverAsync(int? id)
        {
            var find = await GetByIdAsync(id).ConfigureAwait(false);
            return find?.Id > 0;
        }

        public async Task<bool> HasDriverAsync(string document)
        {
            var find = await GetByDocumentAsync(document).ConfigureAwait(false);
            return find?.Id > 0;
        }

        public async Task<IDriver> SaveAsync(IDriver driver)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<int>(
                    DriverStatements.Save,
                    new
                    {
                        AnCdDriver = driver.Id,
                        AcDocument = driver.Document,
                        AcFirstName = driver.FirstName,
                        AcLastName = driver.LastName
                    },
                    commandType: CommandType.StoredProcedure);

                driver.Id = result;

                return driver;
            }
        }
    }
}