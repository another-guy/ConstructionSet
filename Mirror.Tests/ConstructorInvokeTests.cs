using Mirror.Internals;
using Mirror.Tests.TestClasses;
using System;
using Xunit;

namespace Mirror.Tests
{
    public class ConstructorInvokeTests
    {
        [Fact]
        public void TestCtorWithNoArgumentsIsAccessible()
        {
            // Arrange
            // Act
            var result = ConstructorInvoke<ClassWithPrivateCtors>
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
            var result = ConstructorInvoke<ClassWithPrivateCtors>
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
            var result = ConstructorInvoke<ClassWithPrivateCtors>
                .UsingPrivateConstructor(o);

            // Assert
            Assert.NotNull(result);
            Assert.Same(o, result.O);
            Assert.Null(result.S);
        }

        [Fact]
        public void TestThrowsOperationExceptionWhenNoMatchingFound()
        {
            // Arrange
            // Act
            var caught = Assert.Throws<InvalidOperationException>(
                () => ConstructorInvoke<ClassWithPrivateCtors>
                    .UsingPrivateConstructor("s", "s", "s"));
            
            // Assert
            Assert.Equal("Didn't find a constructor mathing passed parameters.", caught.Message);
        }

        [Fact]
        public void TestThrowsOperationExceptionWhenMoreThanOneEquallyFitCtorFound()
        {
            // Arrange
            // Act
            var caught = Assert.Throws<InvalidOperationException>(
                () => ConstructorInvoke<ClassWithPrivateCtors>
                    .UsingPrivateConstructor(1, 1, 1));

            // Assert
            Assert.Equal("Matching mechanism could not choose a best constructor to invoke.", caught.Message);
        }
    }
}
