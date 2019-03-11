using System;

namespace Driver.Application
{
    /// <summary>
    /// Extensions de objeto
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Método responsável por substituir um null por DBNull em uma comunicação com o SQL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object DbNullIfNull(this object value)
        {
            if (value == null)
                return DBNull.Value;

            return value;
        }
    }
}