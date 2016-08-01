using System;
using System.Reflection;

namespace ConstructionSet
{
    public static class Get
    {
        private static readonly BindingFlags privateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        private static readonly BindingFlags privateStatic = BindingFlags.NonPublic | BindingFlags.Static;

        public static object FieldValue<T>(T target, string fieldName)
        {
            return typeof(T).GetField(fieldName, privateInstance).GetValue(target);
        }

        public static object StaticFieldValue<T>(string fieldName)
        {
            return typeof(T).GetField(fieldName, privateStatic).GetValue(null);
        }

        public static object PropertyValue<T>(T target, string propertyName)
        {
            return typeof(T).GetProperty(propertyName, privateInstance).GetValue(target);
        }

        public static object StaticPropertyValue<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, privateStatic).GetValue(null);
        }
    }
}
