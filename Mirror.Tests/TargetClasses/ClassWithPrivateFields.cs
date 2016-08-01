namespace Mirror.Tests.TestClasses
{
    public class ClassWithPrivateFields
    {
        public static int DefaultValue = int.MaxValue;
        public static string InstanceFieldName = nameof(number);
        public static string StaticFieldName = nameof(otherNumber);

        private int number = DefaultValue;
        private static int otherNumber = DefaultValue;
    }
}
