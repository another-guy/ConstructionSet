namespace Mirror.Tests.TestClasses
{
    public class ClassWithPrivateFields
    {
        private static int DefaultValue = int.MaxValue;
        public static string InstanceFieldName = nameof(targetInstanceField);
        public static string StaticFieldName = nameof(targetStaticField);

        private int targetInstanceField = DefaultValue;
        public int DirectInstanceField { get { return targetInstanceField; } }

        private static int targetStaticField = DefaultValue;
        public static int DirectStaticField { get { return targetStaticField; } }
    }
}
