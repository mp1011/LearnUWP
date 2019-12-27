using System;
using System.Linq;

namespace FinancialDucks.Extensions
{
    public static class ReflectionExtensions
    {
        public static TResult DynamicDispatch<TResult>(this object o, string methodName, Type[] typeArguments, object[] args)
        {
            var methodWithName = o
                .GetType()
                .GetMethods()
                .Where(m => m.Name == methodName)
                .ToArray();

            //making the assumption that there will only be one method with this number of generic arguments
            var method = methodWithName.Single(p => p.GetGenericArguments().Length == typeArguments.Length);

            var result  = method.MakeGenericMethod(typeArguments).Invoke(o, args);
            return (TResult)result;
        }
    }
}
