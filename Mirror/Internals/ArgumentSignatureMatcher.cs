using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mirror.Internals
{
    public static class ArgumentSignatureMatcher
    {
        // empty args                          -> parameters ==   null
        // single null arg                     -> parameters == [ null ]
        // first arg null, second arg not null -> parameters == [ null, 123 ]
        public static int CalculateMatchScore(MethodBase method, object[] parameters)
        {
            var parameterInfos = method.GetParameters();
            if (parameterInfos.Length == 0 && parameters.Length == 0)
                return 1000;

            var score = 0;
            if (parameterInfos.Length == parameters.Length)
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameterInfoType = parameterInfos[i].ParameterType;
                    var parameter = parameters[i];

                    if (parameter == null)
                    {
                        if (parameterInfoType.GetTypeInfo().IsClass)
                        {
                            score += 1;
                            continue;
                        }
                        else
                        {
                            // Null is passed, but it can not be assigned to a non-class parameter
                            return 0;
                        }
                    }

                    var parameterType = parameter.GetType();

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
                    // Parameter is not at all assignable
                    else
                        return 0;
                }

            return score;
        }

        public static T FindBestMatching<T>(
            IEnumerable<T> methods,
            object[] parameters)
            where T : MethodBase
        {
            var scores = new Dictionary<int, List<T>>();

            foreach (var method in methods)
            {
                var score = CalculateMatchScore(method, parameters);

                List<T> listForThisScore;
                if (scores.TryGetValue(score, out listForThisScore) == false)
                {
                    listForThisScore = new List<T>();
                    scores.Add(score, listForThisScore);
                }

                listForThisScore.Add(method);
            }
            
            var memberType = typeof(T) == typeof(ConstructorInfo) ? "constructor" : "method";

            var nonZeroScores = scores.Keys.Where(score => score > 0).ToArray();
            if (nonZeroScores.Any() == false)
                throw new InvalidOperationException($"Didn't find a {memberType} mathing passed parameters.");

            var maxScore = nonZeroScores.Max();
            var methodInfos = scores[maxScore];
            if (methodInfos.Count > 1)
                throw new InvalidOperationException($"Matching mechanism could not choose a best {memberType} to invoke.");

            return methodInfos.Single();
        }
    }
}
