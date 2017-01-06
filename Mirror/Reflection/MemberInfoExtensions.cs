using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class MemberInfoExtensions
    {
        [Pure]
        public static IEnumerable<Attribute> GetAttributes<T>(
            this MemberInfo memberInfo,
            bool inherit = true)
            where T : Attribute
        {
            return memberInfo
                .GetAttributes(typeof(T).FullName, inherit);
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
