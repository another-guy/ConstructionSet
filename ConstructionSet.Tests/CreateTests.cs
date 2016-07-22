using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Xunit;

namespace ConstructionSet.Tests
{
    public class CreateTests
    {
        [Fact]
        public void TestCtorWithNoArgumentsIsAccessible()
        {
            // Arrange
            // Act
            var result = Create<ClassWithPrivateCtors>
                .UsingPrivateConstructor();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.O);
            Assert.Null(result.S);
        }

        [Fact]
        public void TestCtorWithObjectArgumentIsAccessible()
        {
            // Arrange
            var s = "a";

            // Act
            var result = Create<ClassWithPrivateCtors>
                .UsingPrivateConstructor(s);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.O);
            Assert.Equal(s, result.S);
        }

        [Fact]
        public void TestCtorWithStringArgumentIsAccessible()
        {
            // Arrange
            var o = new object();

            // Act
            var result = Create<ClassWithPrivateCtors>
                .UsingPrivateConstructor(o);

            // Assert
            Assert.NotNull(result);
            Assert.Same(o, result.O);
            Assert.Null(result.S);
        }

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
            var score = Create<ClassWithPrivateCtors>
                .CalculateMatchScore(ctorInfo, parameters);

            // Assert
            Assert.Equal((uint)expectedScore, score);
        }

        public static IEnumerable<object[]> TestScoreData = new List<object[]>
        {
            // Default ctor always has 0 score
            new object[] { new Type[] { }, new object[] { }, 0 },

            // Parameter types exactly match types in signature
            new object[] { new [] { typeof(string) }, new object[] { "s" }, 1000 },
            new object[] { new [] { typeof(object) }, new object[] { new object() }, 1000 },
            new object[] { new [] { typeof(Parent) }, new object[] { new Parent() }, 1000 },

            // Parameter types are subtypes of the ones in signature
            new object[] { new [] { typeof(Parent) }, new object[] { new Child() }, 10 },

            // Any type is assignable to object
            new object[] { new [] { typeof(object) }, new object[] { "s" }, 1 },
            new object[] { new [] { typeof(object) }, new object[] { 1 }, 1 }
        };
    }
}
