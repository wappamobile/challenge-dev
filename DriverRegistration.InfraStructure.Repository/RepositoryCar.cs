using DriverRegistration.Domain.Factories;
using DriverRegistration.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DriverRegistration.InfraStructure.Repository
{
    public class RepositoryCar: RepositoryBase, IRepositoryCar
    {
        #region Constructors
        public RepositoryCar(IConfiguration configuration) : base(configuration) { }
        #endregion

        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Methods
        public ICar Add(ICar car, int DriverId)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_CAR_ADD";
            cmd.Parameters.Add("@BRAND", System.Data.SqlDbType.VarChar).Value = car.Brand;
            cmd.Parameters.Add("@MODEL", System.Data.SqlDbType.VarChar).Value = car.Model;
            cmd.Parameters.Add("@PLATE", System.Data.SqlDbType.VarChar).Value = car.Plate;
            cmd.Parameters.Add("@DRIVER_ID", System.Data.SqlDbType.Int).Value = DriverId;

            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            connection.Open();
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
                car.Id = (cmd.Parameters["@ID"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@ID"].Value) : 0);
            }
            finally
            {
                connection.Close();
            }

            return car;
        }

        public bool Update(ICar car)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_CAR_UP";
            cmd.Parameters.Add("@BRAND", System.Data.SqlDbType.VarChar).Value = car.Brand;
            cmd.Parameters.Add("@MODEL", System.Data.SqlDbType.VarChar).Value = car.Model;
            cmd.Parameters.Add("@PLATE", System.Data.SqlDbType.VarChar).Value = car.Plate;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = car.Id;
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

        public Boolean Delete(int id)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_CAR_DEL";
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

        public ICar Load(int DriverId)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_CAR_LOAD";
            cmd.Parameters.Add("@DRIVER_ID", System.Data.SqlDbType.Int).Value = DriverId;

            connection.Open();
            cmd.Connection = connection;
            ICar _response = null;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    _response = factoryCar.Create(
                        (dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0),
                        (dr["BRAND"] != DBNull.Value ? dr["BRAND"].ToString() : String.Empty ),
                        (dr["MODEL"] != DBNull.Value ? dr["MODEL"].ToString() : String.Empty),
                        (dr["PLATE"] != DBNull.Value ? dr["PLATE"].ToString() : String.Empty)
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
