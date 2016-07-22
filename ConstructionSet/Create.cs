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
            var scores = new Dictionary<int, List<ConstructorInfo>>();

            foreach (var constructor in constructors)
            {
                var score = ArgumentSignatureMatcher.CalculateMatchScore(constructor, parameters);

                List<ConstructorInfo> listForThisScore;
                if (scores.TryGetValue(score, out listForThisScore) == false)
                {
                    listForThisScore = new List<ConstructorInfo>();
                    scores.Add(score, listForThisScore);
                }

                listForThisScore.Add(constructor);
            }

            var nonZeroScores = scores.Keys.Where(score => score > 0).ToArray();
            if (nonZeroScores.Any() == false)
                throw new InvalidOperationException("Didn't find a constructor mathing passed parameters.");

            var maxScore = nonZeroScores.Max();
            var constructorInfos = scores[maxScore];
            if (constructorInfos.Count > 1)
                throw new InvalidOperationException("Matching mechanism could not choose a best constructor to invoke.");

            return constructorInfos.Single();
        }
    }
}
