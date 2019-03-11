using Driver.Application.Data.Entities;
using Driver.Application.Data.Repositories.Common;
using Driver.Application.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Driver.Application.Data.Repositories
{
    /// <summary>
    /// Estrutura de repository de motorista
    /// </summary>
    public class DriverRepository : RepositoryBase, IDriverRepository
    {
        public DriverRepository(SqlConnectionProvider connection) : base(connection)
        {
        }

        /// <summary>
        /// Método responsável por aparar um motorista (logicamente)
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns>Retorna se foi desabilitado algum motorista</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            using (var cmd = CreateCommand())
            {
                cmd.CommandText = "UPDATE Driver SET Enabled = 0 WHERE DriverId = @DriverId AND Enabled = 1";

                cmd.Parameters.Add("@DriverId", SqlDbType.Int).Value = id;

                return (await cmd.ExecuteNonQueryAsync()) > 0;
            }
        }

        /// <summary>
        /// Método responsável por listar todos os motoristas
        /// </summary>
        /// <returns>Uma lista de motoristas</returns>
        public async Task<List<DriverEntity>> GetAsync()
        {
            using (var cmd = CreateCommand())
            {
                cmd.CommandText = "SELECT DriverId, FirstName, LastName, CarModel, CarBrand, CarLicensePlate, AddressNumber, Address, AddressDistrict, AddressCity, AddressState, AddressLatitude, AddressLongitude, CreatedDate, UpdatedDate, AddressZipCode FROM Driver WITH(NOLOCK) WHERE Enabled = 1";

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<DriverEntity> ret = new List<DriverEntity>();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            ret.Add(new DriverEntity
                            {
                                DriverId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                CarModel = reader.GetString(3),
                                CarBrand = reader.GetString(4),
                                CarLicensePlate = reader.GetString(5),
                                AddressNumber = reader.GetStringNullable(6),
                                Address = reader.GetString(7),
                                District = reader.GetStringNullable(8),
                                City = reader.GetString(9),
                                State = reader.GetString(10),
                                Latitude = reader.GetDoubleNullable(11),
                                Longitude = reader.GetDoubleNullable(12),
                                CreatedDate = reader.GetDateTime(13),
                                UpdatedDate = reader.GetDateTimeNullable(14),
                                ZipCode = reader.GetString(15)
                            });
                        }
                    }

                    return ret;
                }
            }
        }

        /// <summary>
        /// Método responsável por buscar um motorista
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns>Retorna um motorista ou null</returns>
        public async Task<DriverEntity> GetBydIdAsync(int id)
        {
            using (var cmd = CreateCommand())
            {
                cmd.CommandText = "SELECT DriverId, FirstName, LastName, CarModel, CarBrand, CarLicensePlate, AddressNumber, Address, AddressDistrict, AddressCity, AddressState, AddressLatitude, AddressLongitude, CreatedDate, UpdatedDate, AddressZipCode FROM Driver WITH(NOLOCK) WHERE DriverId = @DriverId AND Enabled = 1";

                cmd.Parameters.Add("@DriverId", SqlDbType.Int).Value = id;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        return new DriverEntity
                        {
                            DriverId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            CarModel = reader.GetString(3),
                            CarBrand = reader.GetString(4),
                            CarLicensePlate = reader.GetString(5),
                            AddressNumber = reader.GetStringNullable(6),
                            Address = reader.GetString(7),
                            District = reader.GetStringNullable(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            Latitude = reader.GetDoubleNullable(11),
                            Longitude = reader.GetDoubleNullable(12),
                            CreatedDate = reader.GetDateTime(13),
                            UpdatedDate = reader.GetDateTimeNullable(14),
                            ZipCode = reader.GetString(15)
                        };
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Método responsável por incluir um novo motorista
        /// </summary>
        /// <param name="entity">Dados do motorista</param>
        /// <returns>Retorna a mesma classe do parametro, mas com alterações</returns>
        public async Task<DriverEntity> InsertAsync(DriverEntity entity)
        {
            using (var cmd = CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Driver (FirstName, LastName, CarModel, CarBrand, CarLicensePlate, AddressNumber, Address, AddressDistrict, AddressCity, AddressState, AddressLatitude, AddressLongitude, CreatedDate, Enabled, AddressZipCode) VALUES (@FirstName, @LastName, @CarModel, @CarBrand, @CarLicensePlate, @AddressNumber, @Address, @AddressDistrict, @AddressCity, @AddressState, @AddressLatitude, @AddressLongitude, @CreatedDate, 1, @AddressZipCode) SELECT SCOPE_IDENTITY()";

                cmd.Parameters.Add("@AddressLatitude", SqlDbType.Float).Value = entity.Latitude.DbNullIfNull();
                cmd.Parameters.Add("@AddressLongitude", SqlDbType.Float).Value = entity.Longitude.DbNullIfNull();
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = entity.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = entity.LastName;
                cmd.Parameters.Add("@CarModel", SqlDbType.VarChar).Value = entity.CarModel;
                cmd.Parameters.Add("@CarBrand", SqlDbType.VarChar).Value = entity.CarBrand;
                cmd.Parameters.Add("@CarLicensePlate", SqlDbType.VarChar).Value = entity.CarLicensePlate;
                cmd.Parameters.Add("@AddressNumber", SqlDbType.VarChar).Value = entity.AddressNumber.DbNullIfNull();
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = entity.Address;
                cmd.Parameters.Add("@AddressDistrict", SqlDbType.VarChar).Value = entity.District.DbNullIfNull();
                cmd.Parameters.Add("@AddressCity", SqlDbType.VarChar).Value = entity.City;
                cmd.Parameters.Add("@AddressState", SqlDbType.VarChar).Value = entity.State;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate = DateTime.Now;
                cmd.Parameters.Add("@AddressZipCode", SqlDbType.VarChar).Value = entity.ZipCode.DbNullIfNull();

                entity.DriverId = await cmd.ExecuteScalarAsync<int>();

                return entity;
            }
        }

        /// <summary>
        /// Método responsável por atualizar um motorista
        /// </summary>
        /// <param name="entity">Dados do motorista</param>
        /// <returns>Retorna a mesma classe do parametro, mas com alterações</returns>
        public async Task<DriverEntity> UpdateAsync(DriverEntity entity)
        {
            using (var cmd = CreateCommand())
            {
                cmd.CommandText = "UPDATE Driver SET FirstName = @FirstName, LastName = @LastName, CarModel = @CarModel, CarBrand = @CarBrand, CarLicensePlate = @CarLicensePlate, AddressNumber = @AddressNumber, Address = @Address, AddressDistrict = @AddressDistrict, AddressCity = @AddressCity, AddressState = @AddressState, AddressLatitude = @AddressLatitude, AddressLongitude = @AddressLongitude, UpdatedDate = @UpdatedDate, AddressZipCode = @AddressZipCode WHERE DriverId = @DriverId AND Enabled = 1";

                cmd.Parameters.Add("@DriverId", SqlDbType.Int).Value = entity.DriverId;
                cmd.Parameters.Add("@AddressLatitude", SqlDbType.Float).Value = entity.Latitude.DbNullIfNull();
                cmd.Parameters.Add("@AddressLongitude", SqlDbType.Float).Value = entity.Longitude.DbNullIfNull();
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = entity.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = entity.LastName;
                cmd.Parameters.Add("@CarModel", SqlDbType.VarChar).Value = entity.CarModel;
                cmd.Parameters.Add("@CarBrand", SqlDbType.VarChar).Value = entity.CarBrand;
                cmd.Parameters.Add("@CarLicensePlate", SqlDbType.VarChar).Value = entity.CarLicensePlate;
                cmd.Parameters.Add("@AddressNumber", SqlDbType.VarChar).Value = entity.AddressNumber.DbNullIfNull();
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = entity.Address;
                cmd.Parameters.Add("@AddressDistrict", SqlDbType.VarChar).Value = entity.District.DbNullIfNull();
                cmd.Parameters.Add("@AddressCity", SqlDbType.VarChar).Value = entity.City;
                cmd.Parameters.Add("@AddressState", SqlDbType.VarChar).Value = entity.State;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = entity.UpdatedDate = DateTime.Now;
                cmd.Parameters.Add("@AddressZipCode", SqlDbType.VarChar).Value = entity.ZipCode.DbNullIfNull();

                await cmd.ExecuteNonQueryAsync();

                return entity;
            }
        }
    }
}