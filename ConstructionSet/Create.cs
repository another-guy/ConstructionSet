using System.Linq;
using System.Reflection;

namespace ConstructionSet
{
    public static class Create<T>
    {
        public static T UsingPrivateConstructor(object[] parameters = null)
        {   
            var defaultConstructor =
                typeof(T)
                    .GetTypeInfo()
                    .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                    .First();

            return (T)defaultConstructor.Invoke(parameters);
        }
    }
}
