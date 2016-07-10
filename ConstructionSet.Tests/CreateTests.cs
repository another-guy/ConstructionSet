using Xunit;

namespace ConstructionSet.Tests
{
    public class CreateTests
    {
        [Fact]
        public void TestClassWithCtorThatHasZeroArguments()
        {
            var result = ConstructionSet
                .Create<ClassWithCtorThatHasZeroArguments>
                .UsingPrivateConstructor();

            Assert.NotNull(result);
        }

        [Fact]
        public void TestClassWithCtorThatHasNonZeroArguments()
        {
            var result = ConstructionSet
                .Create<ClassWithCtorThatHasNonZeroArguments>
                .UsingPrivateConstructor(new object[] { 1, "a" });

            Assert.NotNull(result);
        }

        public class ClassWithCtorThatHasZeroArguments
        {
            private ClassWithCtorThatHasZeroArguments()
            {
            }
        }

        public class ClassWithCtorThatHasNonZeroArguments
        {
            private ClassWithCtorThatHasNonZeroArguments(int a, string b)
            {
            }
        }
    }
}
