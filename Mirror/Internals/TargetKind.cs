using System.Reflection;

namespace Mirror.Internals
{
    public class TargetKind
    {
        public static readonly BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        public static readonly BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;
    }
}
