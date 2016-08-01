using System.Reflection;

namespace Mirror
{
    public static class Get
    {
        public static object FieldValue<T>(T target, string fieldName)
        {
            return typeof(T).GetField(fieldName, TargetKind.PrivateInstance).GetValue(target);
        }

        public static object StaticFieldValue<T>(string fieldName)
        {
            return typeof(T).GetField(fieldName, TargetKind.PrivateStatic).GetValue(null);
        }

        public static object PropertyValue<T>(T target, string propertyName)
        {
            return typeof(T).GetProperty(propertyName, TargetKind.PrivateInstance).GetValue(target);
        }

        public static object StaticPropertyValue<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, TargetKind.PrivateStatic).GetValue(null);
        }
    }
}
