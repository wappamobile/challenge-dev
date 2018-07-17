using DriverRegistration.Domain.Factories;
using DriverRegistration.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DriverRegistration.InfraStructure.Repository
{
    public class RepositoryDriver : RepositoryBase, IRepositoryDriver
    {
        #region Constructors
        public RepositoryDriver(IConfiguration configuration) : base(configuration)
        {
            
        }
        #endregion

        #region Attributes
        
        #endregion

        #region Properties

        #endregion

        #region Methods
        public IDriver Add(IDriver driver)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DRIVER_ADD";
            cmd.Parameters.Add("@FIRST_NAME", System.Data.SqlDbType.VarChar).Value = driver.FirstName;
            cmd.Parameters.Add("@LAST_NAME", System.Data.SqlDbType.VarChar).Value = driver.LastName;

            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            connection.Open();
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
                driver.Id = (cmd.Parameters["@ID"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@ID"].Value) : 0);
            }
            finally
            {
                connection.Close();
            }

            return driver;
        }

        public bool Update(IDriver driver)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DRIVER_UP";
            cmd.Parameters.Add("@FIRST_NAME", System.Data.SqlDbType.VarChar).Value = driver.FirstName;
            cmd.Parameters.Add("@LAST_NAME", System.Data.SqlDbType.VarChar).Value = driver.LastName;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = driver.Id;
            cmd.Parameters.Add("@RETURN_CODE", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            connection.Open();
            cmd.Connection = connection;
            int _response = -1;

            try
            {
                cmd.ExecuteNonQuery();
                _response = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
            }
            finally
            {
                connection.Close();
            }

            if (_response > 0)
                return true;
            else
                return false;
        }

        public bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DRIVER_DEL";
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@RETURN_CODE", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            connection.Open();
            cmd.Connection = connection;
            int _response = -1;

            try
            {
                cmd.ExecuteNonQuery();
                _response = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
            }
            finally
            {
                connection.Close();
            }

            if (_response > 0)
                return true;
            else
                return false;
        }

        public IDriver Load(int id)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DRIVER_LOAD";
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;

            connection.Open();
            cmd.Connection = connection;
            IDriver _response = null;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    _response = factoryDriver.Create(
                       id,
                       (dr["FIRST_NAME"] != DBNull.Value ? dr["FIRST_NAME"].ToString() : ""),
                       (dr["LAST_NAME"] != DBNull.Value ? dr["LAST_NAME"].ToString() : ""),
                       factoryAddress.Create(
                           (dr["ADDRESS_ID"] != DBNull.Value ? Convert.ToInt32(dr["ADDRESS_ID"]) : -1),
                           (dr["ADDRESS_NAME"] != DBNull.Value ? dr["ADDRESS_NAME"].ToString() : String.Empty),
                           (dr["ADDRESS_NUMBER"] != DBNull.Value ? Convert.ToInt32(dr["ADDRESS_NUMBER"]) : -1),
                           (dr["ADDRESS_NEIGHBORHOOD"] != DBNull.Value ? dr["ADDRESS_NEIGHBORHOOD"].ToString() : String.Empty),
                           (dr["ADDRESS_POSTAL_CODE"] != DBNull.Value ? dr["ADDRESS_POSTAL_CODE"].ToString() : string.Empty),
                           (dr["ADDRESS_STATE"] != DBNull.Value ? dr["ADDRESS_STATE"].ToString() : string.Empty),
                           (dr["ADDRESS_LONGITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["ADDRESS_LONGITUDE"]) : 0m),
                           (dr["ADDRESS_LATITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["ADDRESS_LATITUDE"]) : 0m),
                           (dr["ADDRESS_CITY"] != DBNull.Value ? dr["ADDRESS_CITY"].ToString() : String.Empty)),
                       factoryCar.Create(
                           (dr["CAR_ID"] != DBNull.Value ? Convert.ToInt32(dr["CAR_ID"]) : -1),
                           (dr["CAR_BRAND"] != DBNull.Value ? dr["CAR_BRAND"].ToString() : String.Empty),
                           (dr["CAR_MODEL"] != DBNull.Value ? dr["CAR_MODEL"].ToString() : String.Empty),
                           (dr["CAR_PLATE"] != DBNull.Value ? dr["CAR_PLATE"].ToString() : String.Empty)
                       )
                   );
                }

            }
            finally
            {
                connection.Close();
            }


            return _response;
        }

        public IEnumerable<IDriver> GetOrderByFirstName(int rowindex, int rowget)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DRIVER_LIST_FIRST_NAME";
            cmd.Parameters.Add("@ROW_INDEX", System.Data.SqlDbType.Int).Value = rowindex;
            cmd.Parameters.Add("@ROW_GET", System.Data.SqlDbType.Int).Value = rowget;

            connection.Open();
            cmd.Connection = connection;
            List<IDriver> _response = null;
            IDriver _driver = null;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                _response = new List<IDriver>();

                while (dr.Read())
                {
                    _driver = factoryDriver.Create(
                        (dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0),
                        (dr["FIRST_NAME"] != DBNull.Value ? dr["FIRST_NAME"].ToString() : ""),
                        (dr["LAST_NAME"] != DBNull.Value ? dr["LAST_NAME"].ToString() : ""),
                        factoryAddress.Create(
                            (dr["ADDRESS_ID"] != DBNull.Value ? Convert.ToInt32(dr["ADDRESS_ID"]) : -1),
                            (dr["ADDRESS_NAME"] != DBNull.Value ? dr["ADDRESS_NAME"].ToString() : String.Empty),
                            (dr["ADDRESS_NUMBER"] != DBNull.Value ? Convert.ToInt32(dr["ADDRESS_NUMBER"]) : -1),
                            (dr["ADDRESS_NEIGHBORHOOD"] != DBNull.Value ? dr["ADDRESS_NEIGHBORHOOD"].ToString() : String.Empty),
                            (dr["ADDRESS_POSTAL_CODE"] != DBNull.Value ? dr["ADDRESS_POSTAL_CODE"].ToString() : string.Empty),
                            (dr["ADDRESS_STATE"] != DBNull.Value ? dr["ADDRESS_STATE"].ToString() : string.Empty),
                            (dr["ADDRESS_LONGITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["ADDRESS_LONGITUDE"]) : 0m),
                            (dr["ADDRESS_LATITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["ADDRESS_LATITUDE"]) : 0m),
                            (dr["ADDRESS_CITY"] != DBNull.Value ? dr["ADDRESS_CITY"].ToString() : String.Empty)),
                        factoryCar.Create(
                            (dr["CAR_ID"] != DBNull.Value ? Convert.ToInt32(dr["CAR_ID"]) : -1),
                            (dr["CAR_BRAND"] != DBNull.Value ? dr["CAR_BRAND"].ToString() : String.Empty),
                            (dr["CAR_MODEL"] != DBNull.Value ? dr["CAR_MODEL"].ToString() : String.Empty),
                            (dr["CAR_PLATE"] != DBNull.Value ? dr["CAR_PLATE"].ToString() : String.Empty)
                        )
                    );

                    _response.Add(_driver);
                }

            }
            finally
            {
                connection.Close();
            }

            return _response;
        }

        public IEnumerable<IDriver> GetOrderByLasttName(int rowindex, int rowget)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DRIVER_LIST_LAST_NAME";
            cmd.Parameters.Add("@ROW_INDEX", System.Data.SqlDbType.Int).Value = rowindex;
            cmd.Parameters.Add("@ROW_GET", System.Data.SqlDbType.Int).Value = rowget;

            connection.Open();
            cmd.Connection = connection;
            List<IDriver> _response = null;
            IDriver _driver = null;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                _response = new List<IDriver>();

                while (dr.Read())
                {
                    _driver = factoryDriver.Create(
                        (dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0),
                        (dr["FIRST_NAME"] != DBNull.Value ? dr["FIRST_NAME"].ToString() : ""),
                        (dr["LAST_NAME"] != DBNull.Value ? dr["LAST_NAME"].ToString() : ""),
                        factoryAddress.Create(
                            (dr["ADDRESS_ID"] != DBNull.Value ? Convert.ToInt32(dr["ADDRESS_ID"]) : -1),
                            (dr["ADDRESS_NAME"] != DBNull.Value ? dr["ADDRESS_NAME"].ToString() : String.Empty),
                            (dr["ADDRESS_NUMBER"] != DBNull.Value ? Convert.ToInt32(dr["ADDRESS_NUMBER"]) : -1),
                            (dr["ADDRESS_NEIGHBORHOOD"] != DBNull.Value ? dr["ADDRESS_NEIGHBORHOOD"].ToString() : String.Empty),
                            (dr["ADDRESS_POSTAL_CODE"] != DBNull.Value ? dr["ADDRESS_POSTAL_CODE"].ToString() : string.Empty),
                            (dr["ADDRESS_STATE"] != DBNull.Value ? dr["ADDRESS_STATE"].ToString():string.Empty),
                            (dr["ADDRESS_LONGITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["ADDRESS_LONGITUDE"]) : 0m),
                            (dr["ADDRESS_LATITUDE"] !=  DBNull.Value ? Convert.ToDecimal(dr["ADDRESS_LATITUDE"]) : 0m),
                            (dr["ADDRESS_CITY"] != DBNull.Value ? dr["ADDRESS_CITY"].ToString() : String.Empty)),
                        factoryCar.Create(
                            (dr["CAR_ID"] !=  DBNull.Value ? Convert.ToInt32(dr["CAR_ID"]) : -1),
                            (dr["CAR_BRAND"] != DBNull.Value ? dr["CAR_BRAND"].ToString() : String.Empty),
                            (dr["CAR_MODEL"] != DBNull.Value ? dr["CAR_MODEL"].ToString() : String.Empty),
                            (dr["CAR_PLATE"] != DBNull.Value ? dr["CAR_PLATE"].ToString() : String.Empty)
                        )
                    );

                    _response.Add(_driver);
                }

            }
            finally
            {
                connection.Close();
            }

            return _response;
        }
        #endregion
    }
}
