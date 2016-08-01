namespace Mirror.Tests.TestClasses
{
    public class ClassWithPrivateFields
    {
        private static int DefaultValue = int.MaxValue;
        public static string InstanceFieldName = nameof(targetInstanceField);
        public static string StaticFieldName = nameof(targetStaticField);
        public static string InstancePropertyName = nameof(TargetInstanceProperty);
        public static string StaticPropertyName = nameof(TargetStaticProperty);

        private int targetInstanceField = DefaultValue;
        public int DirectInstanceField { get { return targetInstanceField; } }

        private static int targetStaticField = DefaultValue;
        public static int DirectStaticField { get { return targetStaticField; } }

        private int TargetInstanceProperty
        {
            get { return DirectInstanceProperty; }
            set { DirectInstanceProperty = value; }
        }
        public int DirectInstanceProperty;

        private static int TargetStaticProperty
        {
            get { return DirectStaticProperty; }
            set { DirectStaticProperty = value; }
        }
        public static int DirectStaticProperty;
    }
}
