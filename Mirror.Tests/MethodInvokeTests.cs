using System;
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

        [Fact]
        public void CallingMethodWithNullForValueTypeParameterThrowsException()
        {
            // Arrange
            // Act
            var caught = Assert.Throws<InvalidOperationException>(() =>
                MethodInvoke.InstanceMethod(
                    target,
                    "InstanceVoidMethodWhichIsNotOkayWhenArgIsNull",
                    new object[] { null }));

            // Assert
            Assert.Equal("Didn't find a method mathing passed parameters.", caught.Message);
        }

        [Fact]
        public void CanCallStaticMethodWithNullArgs()
        {
            // Arrange
            // Act
            var result = (string)MethodInvoke
                .StaticMethod<ClassWithPrivateMethods>(
                    "StaticStringMethodWhichIsOkayWhenArgIsNull",
                    new object[] { null })
                .Value;

            // Assert
            Assert.Equal(result, "Arg is null");
        }

        [Fact]
        public void CallingStaticMethodWithNullForValueTypeParameterThrowsException()
        {
            // Arrange
            // Act
            var caught = Assert.Throws<InvalidOperationException>(() =>
                MethodInvoke.StaticMethod<ClassWithPrivateMethods>(
                    "StaticVoidMethodWhichIsNotOkayWhenArgIsNull",
                    new object[] { null }));

            // Assert
            Assert.Equal("Didn't find a method mathing passed parameters.", caught.Message);
        }
    }
}