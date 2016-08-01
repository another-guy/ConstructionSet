using System.Reflection;

namespace Mirror
{
    public static class Set
    {
        public static void Value<T>(T target, string memberName, object value)
        {
            var type = typeof(T);
            var property = type.GetProperty(memberName, TargetKind.PrivateInstance);
            if (property != null)
                property.SetValue(target, value);
            else
                type.GetField(memberName, TargetKind.PrivateInstance).SetValue(target, value);
        }

        public static void StaticValue<T>(string memberName, object value)
        {
            var type = typeof(T);
            var property = type.GetProperty(memberName, TargetKind.PrivateStatic);
            if (property != null)
                property.SetValue(null, value);
            else
                type.GetField(memberName, TargetKind.PrivateStatic).SetValue(null, value);
        }
    }
}
