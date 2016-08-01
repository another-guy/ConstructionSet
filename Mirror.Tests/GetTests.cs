using Mirror.Tests.TestClasses;
using Xunit;

namespace Mirror.Tests
{
    public class GetTests
    {
        private readonly ClassWithPrivateFields target = new ClassWithPrivateFields();

        [Fact]
        public void CanGetInstanceFieldValue()
        {
            // Arrange
            var fieldName = ClassWithPrivateFields.InstanceFieldName;

            // Act
            var value = Get.Value(target, fieldName);

            // Assert
            Assert.Equal(target.DirectInstanceField, value);
        }

        [Fact]
        public void CanGetStaticFieldValue()
        {
            // Arrange
            var fieldName = ClassWithPrivateFields.StaticFieldName;

            // Act
            var value = Get.StaticValue<ClassWithPrivateFields>(fieldName);

            // Assert
            Assert.Equal(ClassWithPrivateFields.DirectStaticField, value);
        }

        [Fact]
        public void CanGetInstancePropertyValue()
        {
            // Arrange
            var propertyName = ClassWithPrivateFields.InstancePropertyName;

            // Act
            var value = Get.Value(target, propertyName);

            // Assert
            Assert.Equal(target.DirectInstanceProperty, value);
        }

        [Fact]
        public void CanGetStaticPropertyValue()
        {
            // Arrange
            var propertyName = ClassWithPrivateFields.StaticPropertyName;

            // Act
            var value = Get.StaticValue<ClassWithPrivateFields>(propertyName);

            // Assert
            Assert.Equal(ClassWithPrivateFields.DirectStaticProperty, value);
        }
    }
}
