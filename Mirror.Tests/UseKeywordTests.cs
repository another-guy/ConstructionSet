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
                .Set(ClassWithPrivateFields.InstanceFieldName)
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
                .Set(ClassWithPrivateFields.StaticFieldName)
                .Value(setValue);

            var readValue = Use.Target<ClassWithPrivateFields>()
                .Get<int>(ClassWithPrivateFields.StaticFieldName);

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
                .Set(ClassWithPrivateFields.InstancePropertyName)
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
                .Set(ClassWithPrivateFields.StaticPropertyName)
                .Value(setValue);

            var readValue = Use.Target<ClassWithPrivateFields>()
                .Get<int>(ClassWithPrivateFields.StaticPropertyName);

            // Assert
            Assert.Equal(setValue, readValue);
        }

        [Fact]
        public void TestStringStaticMethodCall()
        {
            // Arrange
            // Act
            var result = Use.Target<ClassWithPrivateMethods>()
                .ToCall<string>("StaticStringMethodWithoutArgs");

            // Assert
            Assert.Equal("successStaticStringMethodWithoutArgs", result);
        }


        [Fact]
        public void TestStringInstanceMethodCall()
        {
            // Arrange
            var target = new ClassWithPrivateMethods();

            // Act
            var result = Use.Target(target)
                .ToCall<string>("InstanceStringMethodWithoutArgs");

            // Assert
            Assert.Equal("successInstanceStringMethodWithoutArgs", result);
        }

        [Fact]
        public void TestVoidStaticMethodCall()
        {
            // Arrange
            // Act
            Use.Target<ClassWithPrivateMethods>().ToCall("StaticVoidMethodWithoutArgs");

            // Assert
        }


        [Fact]
        public void TestVoidInstanceMethodCall()
        {
            // Arrange
            var target = new ClassWithPrivateMethods();

            // Act
            Use.Target(target).ToCall("InstanceVoidMethodWithoutArgs");

            // Assert
        }
    }
}
