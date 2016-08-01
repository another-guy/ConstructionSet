using Mirror.Internals;

namespace Mirror.Wrappers
{
    public class InstanceSetter<T>
    {
        private readonly T target;
        private readonly string memberName;

        public InstanceSetter(T target, string memberName)
        {
            this.target = target;
            this.memberName = memberName;
        }

        public void Value(object value)
        {
            FieldPropertyWrite.Value(target, memberName, value);
        }
    }
}
