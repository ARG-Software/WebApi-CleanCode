using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace RF.Library.LinqExtensions
{
    public static class GenericExtensions
    {
        public static object GetPropertyValue<T>(this T obj, string name) where T : class
        {
            var t = typeof(T);
            var property = t.GetProperty(name);
            if (property == null)
            {
                throw new ArgumentException("No matching property");
            }
            var valueToReturn = property.GetValue(obj, null);

            return valueToReturn;
        }

        public static IEnumerable<PropertyInfo> GetObjectPropertiesList<T>() where T : class
        {
            var type = typeof(T);

            var properties = type.GetProperties();

            return properties;
        }

        public static IEnumerable<string> GetObjectBooleanPropertiesNameByValue<T>(this T obj, bool value) where T : class
        {
            var type = typeof(T);

            var properties = type.GetProperties()
                .Where(p => p.PropertyType == typeof(bool) && (bool)p.GetValue(obj, null) == value)
                .Select(p => p.Name);

            return properties;
        }

        public static string GetPropertyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            if (expression.Body is MemberExpression body)
            {
                return body.Member.Name;
            }

            var uBody = (UnaryExpression)expression.Body;
            body = uBody.Operand as MemberExpression;

            return body?.Member.Name;
        }
    }
}