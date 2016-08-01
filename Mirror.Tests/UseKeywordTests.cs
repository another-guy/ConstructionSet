using Mirror.Tests.TestClasses;
using Xunit;

namespace Mirror.Tests
{
    public class UseKeywordTests
    {
        [Fact]
        public void CreateObjectWorks()
        {
            // Arrange
            // Act
            var createdInstance = Use.Target<ClassWithPrivateCtors>()
                .ToCreateInstance();

            // Assert
            Assert.NotNull(createdInstance);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1024)]
        public void TestInstanceFieldGetSet(int setValue)
        {
            // Arrange
            var target = new ClassWithPrivateFields();

            // Act
            Use.Target(target)
                .ToSet(ClassWithPrivateFields.InstanceFieldName)
                .Value(setValue);

            var readValue = Use.Target(target)
                .ToGet<int>(ClassWithPrivateFields.InstanceFieldName);

            // Assert
            Assert.Equal(setValue, readValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1024)]
        public void TestStaticFieldGetSet(int setValue)
        {
            // Arrange
            // Act
            Use.Target<ClassWithPrivateFields>()
                .ToSet(ClassWithPrivateFields.StaticFieldName)
                .Value(setValue);

            var readValue = Use.Target<ClassWithPrivateFields>()
                .ToGet<int>(ClassWithPrivateFields.StaticFieldName);

            // Assert
            Assert.Equal(setValue, readValue);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(1024)]
        public void TestInstancePropertyGetSet(int setValue)
        {
            // Arrange
            var target = new ClassWithPrivateFields();

            // Act
            Use.Target(target)
                .ToSet(ClassWithPrivateFields.InstancePropertyName)
                .Value(setValue);

            var readValue = Use.Target(target)
                .ToGet<int>(ClassWithPrivateFields.InstancePropertyName);

            // Assert
            Assert.Equal(setValue, readValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1024)]
        public void TestStaticPropertyGetSet(int setValue)
        {
            // Arrange
            // Act
            Use.Target<ClassWithPrivateFields>()
                .ToSet(ClassWithPrivateFields.StaticPropertyName)
                .Value(setValue);

            var readValue = Use.Target<ClassWithPrivateFields>()
                .ToGet<int>(ClassWithPrivateFields.StaticPropertyName);

            // Assert
            Assert.Equal(setValue, readValue);
        }

        // TODO FIXME More tests for methods please
        [Fact]
        public void TestStaticMethodCall()
        {
            // Arrange
            // Act
            var result = Use.Target<ClassWithPrivateMethods>()
                .ToCall<string>("StaticStringMethodWithoutArgs");

            // Assert
            Assert.Equal("successStaticStringMethodWithoutArgs", result);
        }
    }
}
