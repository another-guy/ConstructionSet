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
            var scores = new Dictionary<uint, List<ConstructorInfo>>();

            foreach (var constructor in constructors)
            {
                var score = CalculateMatchScore(constructor, parameters);

                List<ConstructorInfo> listForThisScore;
                if (scores.TryGetValue(score, out listForThisScore) == false)
                {
                    listForThisScore = new List<ConstructorInfo>();
                    scores.Add(score, listForThisScore);
                }

                listForThisScore.Add(constructor);
            }

            var maxScore = scores.Keys.Max();
            var constructorInfos = scores[maxScore];
            switch (constructorInfos.Count)
            {
                // TODO Unit test exceptions
                case 0:
                    throw new InvalidOperationException(
                        "Didn't find a constructor mathing passed parameters.");
                case 1:
                    break;
                default:
                    throw new InvalidOperationException(
                        "Matching mechanism could not choose a best constructor to invoke. " +
                        constructorInfos.Count + " constructos have conflicting score " + maxScore + ".");
            }

            var candidateCtor = constructorInfos.Single();
            if (candidateCtor.GetParameters().Length != parameters.Length)
                throw new InvalidOperationException(""); // TODO What about varargs/params? TODO Unit test too
            return candidateCtor;
        }

        // TODO Move to separate class
        public static uint CalculateMatchScore(MethodBase method, object[] parameters)
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
                    else if (parameterInfoType.IsAssignableFrom(parameterType) &&
                        parameterInfoType != typeof(object))
                        score += 10;
                    // Ctor parameter is object type and therefore can be used with any argument
                    else if (parameterInfoType.IsAssignableFrom(parameterType) &&
                        parameterInfoType == typeof(object))
                        score += 1;
                }

            return score;
        }
    }
}
