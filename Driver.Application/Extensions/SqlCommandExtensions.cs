using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Driver.Application
{
    /// <summary>
    /// Extensions de comando sql
    /// </summary>
    public static class SqlCommandExtensions
    {
        /// <summary>
        /// Retorna valor scalar tipado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(this SqlCommand cmd) where T : IConvertible
        {
            var ret = cmd.ExecuteScalar();

            if (ret != null && ret != DBNull.Value)
            {
                return (T)Convert.ChangeType(ret, typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Retorna um valor scalar tipado assincronamente
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsync<T>(this SqlCommand cmd) where T : IConvertible
        {
            var ret = await cmd.ExecuteScalarAsync();

            if (ret != null && ret != DBNull.Value)
            {
                return (T)Convert.ChangeType(ret, typeof(T));
            }

            return default(T);
        }
    }
}