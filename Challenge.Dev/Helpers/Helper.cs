using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Challenge.Dev.Helpers
{
    public static class Helper
    {
        private const string URL_API_GOOGLE_MAPS = "http://maps.google.com/maps/api/geocode/json?address=";
        public static T GetGeocode<T>(params string[] values)
        {           
            using (HttpClient client = new HttpClient())
            {
                
                Task<HttpResponseMessage> response = client.GetAsync($"{ URL_API_GOOGLE_MAPS }{ string.Join("+", values) }");
                response.Wait();

                Task<string> responseString = response.Result.Content.ReadAsStringAsync();
                responseString.Wait();

                return JsonConvert.DeserializeObject<T>(responseString.Result);
            }
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            System.Reflection.PropertyInfo pi = typeof(T).GetProperties().FirstOrDefault(x => x.Name.ToLower() == sortBy.ToLower());
            pi = pi ?? typeof(T).GetProperties().FirstOrDefault();

            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");

            return (IOrderedQueryable<T>)source.Provider.CreateQuery(
                   Expression.Call(
                    typeof(Queryable),
                      "OrderBy",
                    new Type[] { typeof(T), pi.PropertyType },
                    source.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
                   );
        }
    }
  
}
