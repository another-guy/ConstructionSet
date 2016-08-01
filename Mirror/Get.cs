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
    }
}
