using NSubstitute;
using Xunit;

namespace Mirror.Tests
{
    public class CallTests
    {
        private readonly ClassWithPrivateMethods target = new ClassWithPrivateMethods();
        private readonly ITrackable trackable = Substitute.For<ITrackable>();

        [Fact]
        public void CanCallInstanceMethodWithArgsButWithoutResult()
        {
            // Arrange
            // Act
            var result = Call.InstanceMethod(target, "InstanceVoidMethodWithArgs", trackable);

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
            var result = Call.InstanceMethod(target, "InstanceStringMethodWithArgs", trackable);

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallInstanceMethodWithoutArgsAndWithoutResult()
        {
            // Arrange
            ClassWithPrivateMethods.GlobalTrackable = Substitute.For<ITrackable>();

            // Act
            var result = Call.InstanceMethod(target, "InstanceVoidMethodWithoutArgs");

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
            ClassWithPrivateMethods.GlobalTrackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallInstanceMethodWithoutArgsButWithResult()
        {
            // Arrange
            ClassWithPrivateMethods.GlobalTrackable = Substitute.For<ITrackable>();

            // Act
            var result = Call.InstanceMethod(target, "InstanceStringMethodWithoutArgs");

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            ClassWithPrivateMethods.GlobalTrackable.Received(1).Touch();
        }


        [Fact]
        public void CanCallStaticMethodWithArgsButWithoutResult()
        {
            // Arrange
            // Act
            var result = Call.StaticMethod(target, "StaticVoidMethodWithArgs", trackable);

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
            var result = Call.StaticMethod(target, "StaticStringMethodWithArgs", trackable);

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallStaticMethodWithoutArgsAndWithoutResult()
        {
            // Arrange
            ClassWithPrivateMethods.GlobalTrackable = Substitute.For<ITrackable>();

            // Act
            var result = Call.StaticMethod(target, "StaticVoidMethodWithoutArgs");

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
            ClassWithPrivateMethods.GlobalTrackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallStaticMethodWithoutArgsButWithResult()
        {
            // Arrange
            ClassWithPrivateMethods.GlobalTrackable = Substitute.For<ITrackable>();

            // Act
            var result = Call.StaticMethod(target, "StaticStringMethodWithoutArgs");

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            ClassWithPrivateMethods.GlobalTrackable.Received(1).Touch();
        }
    }
}