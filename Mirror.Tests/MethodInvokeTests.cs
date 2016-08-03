using Mirror.Internals;
using Mirror.Tests.TestClasses;
using NSubstitute;
using Xunit;

namespace Mirror.Tests
{
    public class MethodInvokeTests
    {
        private readonly ClassWithPrivateMethods target = new ClassWithPrivateMethods();
        private readonly ITrackable trackable = Substitute.For<ITrackable>();

        [Fact]
        public void CanCallInstanceMethodWithArgsButWithoutResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke.InstanceMethod(target, "InstanceVoidMethodWithArgs", new object[] { trackable });

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallInstanceMethodWithArgsAndWithResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke.InstanceMethod(target, "InstanceStringMethodWithArgs", new object[] { trackable });

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallInstanceMethodWithoutArgsAndWithoutResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke.InstanceMethod(target, "InstanceVoidMethodWithoutArgs", new object[0]);

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
        }

        [Fact]
        public void CanCallInstanceMethodWithoutArgsButWithResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke.InstanceMethod(target, "InstanceStringMethodWithoutArgs", new object[0]);

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("successInstanceStringMethodWithoutArgs", result.Value);
        }


        [Fact]
        public void CanCallStaticMethodWithArgsButWithoutResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke
                .StaticMethod<ClassWithPrivateMethods>("StaticVoidMethodWithArgs", new object[] { trackable });

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallStaticMethodWithArgsAndWithResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke
                .StaticMethod<ClassWithPrivateMethods>("StaticStringMethodWithArgs", new object[] { trackable });

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("successStaticStringMethodWithArgs", result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallStaticMethodWithoutArgsAndWithoutResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke
                .StaticMethod<ClassWithPrivateMethods>("StaticVoidMethodWithoutArgs", new object[0]);

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
        }

        [Fact]
        public void CanCallStaticMethodWithoutArgsButWithResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke
                .StaticMethod<ClassWithPrivateMethods>("StaticStringMethodWithoutArgs", new object[0]);

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("successStaticStringMethodWithoutArgs", result.Value);
        }


        // TODO Add same test that will have null passed to a value type parameter (FAIL)
        // TODO Add same test (with null) for static method
        // TODO Add test that does not need parameters at all
        [Fact]
        public void CanCallMethodWithNullArgs()
        {
            // Arrange
            // Act
            var result = (string)MethodInvoke
                .InstanceMethod(target, "InstanceStringMethodWhichIsOkayWhenArgIsNull", new object[] { null } )
                .Value;

            // Assert
            Assert.Equal(result, "Arg is null");
        }
    }
}