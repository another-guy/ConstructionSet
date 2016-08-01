using System.Reflection;

namespace ConstructionSet
{
    public static class Set
    {
        public static void FieldValue<T>(T target, string fieldName, object value)
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(target, value);
        }

        public static void StaticFieldValue<T>(string fieldName, object value)
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(null, value);
        }
    }
}
