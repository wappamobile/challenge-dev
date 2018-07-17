using DriverRegistration.Domain.Factories;
using DriverRegistration.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DriverRegistration.InfraStructure.Repository
{
    public class RepositoryAddress: RepositoryBase, IRepositoryAddress
    {
        #region Constructors
        public RepositoryAddress(IConfiguration configuration) : base(configuration) { }
        #endregion

        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Methods
        public IAddress Add(IAddress address, int DriverId)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_ADDRESS_ADD";
            cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar).Value = address.AddressName;
            cmd.Parameters.Add("@NUMBER", System.Data.SqlDbType.Int).Value = address.Number;
            cmd.Parameters.Add("@NEIGHBORHOOD", System.Data.SqlDbType.VarChar).Value = address.Neighborhood;
            cmd.Parameters.Add("@POSTAL_CODE", System.Data.SqlDbType.VarChar).Value = address.PostalCode;
            cmd.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar).Value = address.State;
            cmd.Parameters.Add("@LONGITUDE", System.Data.SqlDbType.Decimal).Value = address.Longitude;
            cmd.Parameters.Add("@LATITUDE", System.Data.SqlDbType.Decimal).Value = address.Latitude;
            cmd.Parameters.Add("@DRIVER_ID", System.Data.SqlDbType.Int).Value = DriverId;
            cmd.Parameters.Add("@CITY", System.Data.SqlDbType.VarChar).Value = address.City;

            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            connection.Open();
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
                address.Id = (cmd.Parameters["@ID"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@ID"].Value) : 0);
            }
            finally
            {
                connection.Close();
            }

            return address;
        }

        public bool Update(IAddress address)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_ADDRESS_UP";
            cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar).Value = address.AddressName;
            cmd.Parameters.Add("@NUMBER", System.Data.SqlDbType.Int).Value = address.Number;
            cmd.Parameters.Add("@NEIGHBORHOOD", System.Data.SqlDbType.VarChar).Value = address.Neighborhood;
            cmd.Parameters.Add("@POSTAL_CODE", System.Data.SqlDbType.VarChar).Value = address.PostalCode;
            cmd.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar).Value = address.State;
            cmd.Parameters.Add("@LONGITUDE", System.Data.SqlDbType.Decimal).Value = address.Longitude;
            cmd.Parameters.Add("@LATITUDE", System.Data.SqlDbType.Decimal).Value = address.Latitude;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = address.Id;
            cmd.Parameters.Add("@CITY", System.Data.SqlDbType.VarChar).Value = address.City;
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
            cmd.CommandText = "SP_ADDRESS_DEL";
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

        public IAddress Load(int DriverId)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_ADDRESS_LOAD";
            cmd.Parameters.Add("@DRIVER_ID", System.Data.SqlDbType.Int).Value = DriverId;

            connection.Open();
            cmd.Connection = connection;
            IAddress _response = null;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    _response = factoryAddress.Create(
                        (dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0),
                        (dr["NAME"] != DBNull.Value ? dr["NAME"].ToString() : String.Empty),
                        (dr["NUMBER"] != DBNull.Value ? Convert.ToInt32(dr["NUMBER"]) : 0),
                        (dr["NEIGHBORHOOD"] != DBNull.Value ? dr["NEIGHBORHOOD"].ToString() : String.Empty),
                        (dr["POSTAL_CODE"] != DBNull.Value ? dr["POSTAL_CODE"].ToString() : String.Empty),
                        (dr["STATE"] != DBNull.Value ? dr["STATE"].ToString() : String.Empty),
                        (dr["LONGITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["LONGITUDE"]) : 0m),
                        (dr["LATITUDE"] != DBNull.Value ? Convert.ToDecimal(dr["LATITUDE"]) : 0m),
                        (dr["CITY"] != DBNull.Value ? dr["CITY"].ToString() : String.Empty)
                    );
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
