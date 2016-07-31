using NSubstitute;
using Xunit;

namespace ConstructionSet.Tests
{
    public class CallTests
    {
        private readonly ClassWithPrivateMethods target = new ClassWithPrivateMethods();
        private readonly ITrackable trackable = Substitute.For<ITrackable>();

        [Fact]
        public void CanCallMethodWithArgsButWithoutResult()
        {
            // Arrange
            // Act
            var result = Call.InstanceMethod(target, "Method1", trackable);

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallMethodWithArgsAndWithResult()
        {
            // Arrange
            // Act
            var result = Call.InstanceMethod(target, "Method2", trackable);

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            trackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallMethodWithoutArgsAndWithoutResult()
        {
            // Arrange
            ClassWithPrivateMethods.GlobalTrackable = Substitute.For<ITrackable>();

            // Act
            var result = Call.InstanceMethod(target, "Method3");

            // Assert
            Assert.False(result.HasResult);
            Assert.Null(result.Value);
            ClassWithPrivateMethods.GlobalTrackable.Received(1).Touch();
        }

        [Fact]
        public void CanCallMethodWithoutArgsButWithResult()
        {
            // Arrange
            ClassWithPrivateMethods.GlobalTrackable = Substitute.For<ITrackable>();

            // Act
            var result = Call.InstanceMethod(target, "Method4");

            // Assert
            Assert.True(result.HasResult);
            Assert.Equal("success", result.Value);
            ClassWithPrivateMethods.GlobalTrackable.Received(1).Touch();
        }
    }
}
