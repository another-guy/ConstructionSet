using Xunit;

namespace ConstructionSet.Tests
{
    public class CreateTests
    {
        [Fact]
        public void TestClassWithCtorThatHasZeroArguments()
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
        public void TestClassWithCtorThatHasNonZeroArguments()
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
        public void TestClassWithCtorThatHasNonZeroArguments2()
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

        public class ClassWithPrivateCtors
        {
            public string S { get; set; }
            public object O { get; set; }

            private ClassWithPrivateCtors()
            {
            }

            private ClassWithPrivateCtors(string s)
            {
                S = s;
            }

            private ClassWithPrivateCtors(object o)
            {
                O = o;
            }
        }
    }
}
