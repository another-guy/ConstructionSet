using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Mirror.Tests
{
    public class MemberInfoExtensionsTests
    {
        [Fact]
        public void CanExtractClassAttributeWhenPresent()
        {
            // Arrange
#pragma warning disable 618
            var target = typeof(TargetClassWithAttributes);
#pragma warning restore 618

            // Act
            var attributes = target.GetAttributes("Mirror.Tests.DemoAttribute");

            // Assert
            Assert.NotNull(attributes.Single() as DemoAttribute);
        }

        [Fact]
        public void CanExtractClassAttributeWhenNotPresent()
        {
            // Arrange
#pragma warning disable 618
            var target = typeof(TargetClassWithoutAttributes);
#pragma warning restore 618

            // Act
            var attributes = target.GetAttributes("Mirror.Tests.DemoAttribute");

            // Assert
            Assert.Equal(0, attributes.Count());
        }

        [Fact]
        public void CanExtractMultpileClassAttributesWhenPresent()
        {
            // Arrange
#pragma warning disable 618
            var target = typeof(DemoTargetClass);
#pragma warning restore 618

            // Act
            var attributes = target.GetAttributes("Mirror.Tests.DemoAttribute");

            // Assert
            Assert.Equal(3, attributes.Count());
        }

        [Fact]
        public void CanExtractMethodAttributeWhenPresent()
        {
            // Arrange
#pragma warning disable 618
            var target = typeof(TargetClassWithAttributes).GetMethod("TargetMethod");
#pragma warning restore 618

            // Act
            var attributes = target.GetAttributes("Mirror.Tests.DemoAttribute");

            // Assert
            Assert.NotNull(attributes.Single() as DemoAttribute);
        }

        [Fact]
        public void CanExtractMethodAttributeWhenNotPresent()
        {
            // Arrange
#pragma warning disable 618
            var target = typeof(TargetClassWithoutAttributes).GetMethod("TargetMethod");
#pragma warning restore 618

            // Act
            var attributes = target.GetAttributes("Mirror.Tests.DemoAttribute");

            // Assert
            Assert.Equal(0, attributes.Count());
        }
        
        [Fact]
        public void CanExtractMultpileMethodAttributesWhenPresent()
        {
            // Arrange
#pragma warning disable 618
            var target = typeof(DemoTargetClass).GetMethod("TargetMethod");
#pragma warning restore 618

            // Act
            var attributes = target.GetAttributes("Mirror.Tests.DemoAttribute");

            // Assert
            Assert.Equal(2, attributes.Count());
        }
    }

    [Demo]
    public class TargetClassWithAttributes
    {
        [Demo]
        public void TargetMethod() { }
    }
    
    public class TargetClassWithoutAttributes
    {
        public void TargetMethod() { }
    }
    
    [Demo]
    [Demo]
    [Demo]
    public class DemoTargetClass
    {
        [Demo]
        [Demo]
        public void TargetMethod() { }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class DemoAttribute : Attribute { }
}
