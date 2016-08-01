using System.Reflection;

namespace Mirror.Internals
{
    public static class FieldPropertyRead
    {
        public static object Value<T>(T target, string memberName)
        {
            var type = typeof(T);
            var property = type.GetProperty(memberName, TargetKind.PrivateInstance);
            if (property != null)
                return property.GetValue(target);
            else
                return type.GetField(memberName, TargetKind.PrivateInstance).GetValue(target);
        }

        public static object StaticValue<T>(string memberName)
        {
            var type = typeof(T);
            var property = type.GetProperty(memberName, TargetKind.PrivateStatic);
            if (property != null)
                return property.GetValue(null);
            else
                return type.GetField(memberName, TargetKind.PrivateStatic).GetValue(null);
        }
    }
}
