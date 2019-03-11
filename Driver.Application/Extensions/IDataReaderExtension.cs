using System;
using System.Data;

namespace Driver.Application
{
    /// <summary>
    /// Extencions de data reader
    /// </summary>
    public static class IDataReaderExtension
    {
        /// <summary>
        /// Retorna um DateTime nullable
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? GetDateTimeNullable(this IDataReader reader, int index, DateTime? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDateTime(index);
        }

        /// <summary>
        /// Retorna null se for necessário de uma string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetStringNullable(this IDataReader reader, int index, string defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetString(index).Trim();
        }

        /// <summary>
        /// Retorna null se for necessário de uma string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double? GetDoubleNullable(this IDataReader reader, int index, double? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDouble(index);
        }
    }
}