using System.Reflection;

namespace Mirror
{
    public static class Set
    {
        public static void FieldValue<T>(T target, string fieldName, object value)
        {
            typeof(T).GetField(fieldName, TargetKind.PrivateInstance).SetValue(target, value);
        }

        public static void StaticFieldValue<T>(string fieldName, object value)
        {
            typeof(T).GetField(fieldName, TargetKind.PrivateStatic).SetValue(null, value);
        }

        public static void PropertyValue<T>(T target, string propertyName, int value)
        {
            typeof(T).GetProperty(propertyName, TargetKind.PrivateInstance).SetValue(target, value);
        }

        public static void StaticPropertyValue<T>(string propertyName, int value)
        {
            typeof(T).GetProperty(propertyName, TargetKind.PrivateStatic).SetValue(null, value);
        }
    }
}
