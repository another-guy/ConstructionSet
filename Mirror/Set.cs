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
    }
}
