using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Xunit;

namespace Mirror.Tests
{
    public class ArgumentSignatureMatcherTests
    {
        [Theory]
        [MemberData(nameof(TestScoreData))]
        public void TestScoresCorrect(Type[] types, object[] parameters, int expectedScore)
        {
            // Arrange
            var ctorInfo = typeof(ClassWithPrivateCtors)
                .GetTypeInfo()
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .Single(c =>
                {
                    var parameterInfos = c.GetParameters();
                    return parameterInfos
                        .Select(p => p.ParameterType)
                        .SequenceEqual(types);
                });

            // Act
            var score = ArgumentSignatureMatcher
                .CalculateMatchScore(ctorInfo, parameters);

            // Assert
            Assert.Equal(expectedScore, score);
        }

        public static IEnumerable<object[]> TestScoreData = new List<object[]>
        {
            // Default ctor always has 0 score
            new object[] { new Type[] { }, new object[] { }, 1000 },

            // Parameter types exactly match types in signature
            // 1 arg
            new object[] { new [] { typeof(string) }, new object[] { "s" }, 1000 },
            new object[] { new [] { typeof(object) }, new object[] { new object() }, 1000 },
            new object[] { new [] { typeof(Parent) }, new object[] { new Parent() }, 1000 },
            // 2 args
            new object[] { new [] { typeof(int), typeof(string) }, new object[] { 1, "s" }, 2000 },
            new object[] { new [] { typeof(int), typeof(object) }, new object[] { 1, new object() }, 2000 },
            new object[] { new [] { typeof(int), typeof(Parent) }, new object[] { 1, new Parent() }, 2000 },

            // Parameter types are subtypes of the ones in signature
            // 1 arg
            new object[] { new [] { typeof(Parent) }, new object[] { new Child() }, 10 },
            // 2 args
            new object[] { new [] { typeof(int), typeof(Parent) }, new object[] { 1, new Child() }, 1010 },

            // Any type is assignable to object
            // 1 arg
            new object[] { new [] { typeof(object) }, new object[] { "s" }, 1 },
            new object[] { new [] { typeof(object) }, new object[] { 1 }, 1 },
            // 2 args
            new object[] { new [] { typeof(int), typeof(object) }, new object[] { 1, "s" }, 1001 },
            new object[] { new [] { typeof(int), typeof(object) }, new object[] { 1, 1 }, 1001 }
        };
    }
}
