using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mirror
{
    public static class Create<T>
    {
        public static T UsingPrivateConstructor(params object[] parameters)
        {   
            var constructors =
                typeof(T)
                    .GetTypeInfo()
                    .GetConstructors(TargetKind.PrivateInstance)
                    .ToList();

            var constructor = FindCtorBySignature(constructors, parameters);

            return (T)constructor.Invoke(parameters);
        }

        private static ConstructorInfo FindCtorBySignature(
            IEnumerable<ConstructorInfo> constructors,
            object[] parameters)
        {
            return parameters.Length == 0 ?
                FindDefaultCtor(constructors) :
                ArgumentSignatureMatcher.FindBestMatching(constructors, parameters);
        }

        private static ConstructorInfo FindDefaultCtor(IEnumerable<ConstructorInfo> constructors)
        {
            return constructors.Single(ctor => ctor.GetParameters().Length == 0);
        }
    }
}
