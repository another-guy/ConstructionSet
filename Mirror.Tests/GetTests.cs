using Mirror.Tests.TestClasses;
using Xunit;

namespace ConstructionSet.Tests
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
            var value = Get.FieldValue(target, fieldName);

            // Assert
            Assert.Equal(target.DirectInstanceField, value);
        }

        [Fact]
        public void CanGetStaticFieldValue()
        {
            // Arrange
            var fieldName = ClassWithPrivateFields.StaticFieldName;

            // Act
            var value = Get.StaticFieldValue<ClassWithPrivateFields>(fieldName);

            // Assert
            Assert.Equal(ClassWithPrivateFields.DirectStaticField, value);
        }
    }
}
