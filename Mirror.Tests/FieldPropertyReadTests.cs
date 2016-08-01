using Mirror.Tests.TestClasses;
using Mirror.Internals;
using Xunit;

namespace Mirror.Tests
{
    public class FieldPropertyReadTests
    {
        private readonly ClassWithPrivateFields target = new ClassWithPrivateFields();

        [Fact]
        public void CanGetInstanceFieldValue()
        {
            // Arrange
            var fieldName = ClassWithPrivateFields.InstanceFieldName;

            // Act
            var value = FieldPropertyRead.Value(target, fieldName);

            // Assert
            Assert.Equal(target.DirectInstanceField, value);
        }

        [Fact]
        public void CanGetStaticFieldValue()
        {
            // Arrange
            var fieldName = ClassWithPrivateFields.StaticFieldName;

            // Act
            var value = FieldPropertyRead.StaticValue<ClassWithPrivateFields>(fieldName);

            // Assert
            Assert.Equal(ClassWithPrivateFields.DirectStaticField, value);
        }

        [Fact]
        public void CanGetInstancePropertyValue()
        {
            // Arrange
            var propertyName = ClassWithPrivateFields.InstancePropertyName;

            // Act
            var value = FieldPropertyRead.Value(target, propertyName);

            // Assert
            Assert.Equal(target.DirectInstanceProperty, value);
        }

        [Fact]
        public void CanGetStaticPropertyValue()
        {
            // Arrange
            var propertyName = ClassWithPrivateFields.StaticPropertyName;

            // Act
            var value = FieldPropertyRead.StaticValue<ClassWithPrivateFields>(propertyName);

            // Assert
            Assert.Equal(ClassWithPrivateFields.DirectStaticProperty, value);
        }
    }
}
