using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class MemberInfoExtensions
    {
        [Pure]
        public static IEnumerable<T> GetAttributes<T>(
            this MemberInfo memberInfo,
            bool inherit = true)
            where T : Attribute
        {
            return memberInfo
                .GetAttributes(typeof(T).FullName, inherit)
                .Cast<T>();
        }

        [Pure]
        public static IEnumerable<Attribute> GetAttributes(
            this Type type,
            string attributeType,
            bool inherit = true)
        {
            return type
                .GetTypeInfo()
                .GetAttributes(attributeType, inherit);
        }

        [Pure]
        public static IEnumerable<T> GetAttributes<T>(
            this Type type,
            bool inherit = true)
            where T : Attribute
        {
            return type
                .GetTypeInfo()
                .GetAttributes(typeof(T).FullName, inherit)
                .Cast<T>();
        }

        [Pure]
        public static IEnumerable<Attribute> GetAttributes(
            this MemberInfo memberInfo,
            string attributeTypeFullName,
            bool inherit = true)
        {
            return memberInfo
                .GetCustomAttributes(inherit)
                .Where(attributeType => attributeType.GetType().FullName == attributeTypeFullName);
        }
    }
}
