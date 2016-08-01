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
            var result = MethodInvoke.InstanceMethod(target, "InstanceVoidMethodWithArgs", trackable);

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
            var result = MethodInvoke.InstanceMethod(target, "InstanceStringMethodWithArgs", trackable);

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
            var result = MethodInvoke.InstanceMethod(target, "InstanceVoidMethodWithoutArgs");

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
        }

        [Fact]
        public void CanCallInstanceMethodWithoutArgsButWithResult()
        {
            // Arrange
            // Act
            var result = MethodInvoke.InstanceMethod(target, "InstanceStringMethodWithoutArgs");

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
                .StaticMethod<ClassWithPrivateMethods>("StaticVoidMethodWithArgs", trackable);

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
                .StaticMethod<ClassWithPrivateMethods>("StaticStringMethodWithArgs", trackable);

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
                .StaticMethod<ClassWithPrivateMethods>("StaticVoidMethodWithoutArgs");

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
                .StaticMethod<ClassWithPrivateMethods>("StaticStringMethodWithoutArgs");

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("successStaticStringMethodWithoutArgs", result.Value);
        }
    }
}