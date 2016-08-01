using System;
using System.Reflection;

namespace ConstructionSet
{
    public static class Get
    {
        public static object FieldValue<T>(T target, string fieldName)
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return field.GetValue(target);
        }

        public static object StaticFieldValue<T>(string fieldName)
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            return field.GetValue(null);
        }

        public static object PropertyValue<T>(T target, string propertyName)
        {
            var field = typeof(T).GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            return field.GetValue(target);
        }

        public static object StaticPropertyValue<T>(string propertyName)
        {
            var field = typeof(T).GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Static);
            return field.GetValue(null);
        }
    }
}
