using System.Text;

namespace Driver.Application.Util
{
    /// <summary>
    /// Utilitários de uri
    /// </summary>
    public static class UriUtil
    {
        /// <summary>
        /// Método responsável por montar uma query de uri
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToQueryString(params (string, string)[] values)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var (key, value) in values)
            {
                if (value != null)
                {
                    if (builder.Length > 0)
                        builder.Append('&');

                    builder.Append(key).Append('=').Append(value);
                }
            }

            if (builder.Length > 0)
                builder.Insert(0, '?');

            return builder.ToString();
        }
    }
}