using System;
using System.Reflection;

namespace ConstructionSet
{
    public static class Set
    {
        private static readonly BindingFlags privateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        private static readonly BindingFlags privateStatic = BindingFlags.NonPublic | BindingFlags.Static;

        public static void FieldValue<T>(T target, string fieldName, object value)
        {
            typeof(T).GetField(fieldName, privateInstance).SetValue(target, value);
        }

        public static void StaticFieldValue<T>(string fieldName, object value)
        {
            typeof(T).GetField(fieldName, privateStatic).SetValue(null, value);
        }

        public static void PropertyValue<T>(T target, string propertyName, int value)
        {
            typeof(T).GetProperty(propertyName, privateInstance).SetValue(target, value);
        }

        public static void StaticPropertyValue<T>(string propertyName, int value)
        {
            typeof(T).GetProperty(propertyName, privateStatic).SetValue(null, value);
        }
    }
}
