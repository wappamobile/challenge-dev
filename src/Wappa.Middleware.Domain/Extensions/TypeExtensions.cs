using System;
using System.Reflection;

namespace Wappa.Middleware.Domain.Extensions
{
    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            var result = type.GetTypeInfo().Assembly;
            return result;
        }
    }
}
