using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;


namespace Infra.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");

            var sortExpression = string.Empty;

            try
            {
                source = source.OrderBy(sortBy);
            }
            catch (ParseException ex)
            {
                throw ex;
            }

            return source;
        }

   
    }
}
