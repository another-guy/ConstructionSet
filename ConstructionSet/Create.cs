using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConstructionSet
{
    public static class Create<T>
    {
        public static T UsingPrivateConstructor(params object[] parameters)
        {   
            var constructors =
                typeof(T)
                    .GetTypeInfo()
                    .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
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
                FindBestMatchingCtor(constructors, parameters);
        }

        private static ConstructorInfo FindDefaultCtor(IEnumerable<ConstructorInfo> constructors)
        {
            return constructors.Single(ctor => ctor.GetParameters().Length == 0);
        }

        private static ConstructorInfo FindBestMatchingCtor(
            IEnumerable<ConstructorInfo> constructors,
            object[] parameters)
        {
            var scores = constructors
                .ToDictionary(constructor => CalculateMatchScore(constructor, parameters));

            var candidateCtor = scores[scores.Keys.Max()];
            if (candidateCtor.GetParameters().Length != parameters.Length)
                throw new InvalidOperationException(""); // TODO What about varargs/params? TODO Unit test too
            return candidateCtor;
        }

        private static uint CalculateMatchScore(MethodBase method, object[] parameters)
        {
            uint score = 0;
            var parameterInfos = method.GetParameters();
            if (parameterInfos.Length == parameters.Length)
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameterInfoType = parameterInfos[i].ParameterType;
                    var parameterType = parameters[i].GetType();

                    // Types exactly match each other
                    if (parameterInfoType == parameterType)
                        score += 1000;
                    // Types are in "Is A" relationship
                    else if (parameterType.IsInstanceOfType(parameterInfoType))
                        score += 10;
                    // Ctor parameter is object type and therefore can be used with any argument
                    else if (parameterInfoType == typeof(object))
                        score += 1;
                }

            return score;
        }
    }
}
