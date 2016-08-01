using Mirror.Internals;

namespace Mirror.Wrappers
{
    public class StaticSetter<T>
    {
        private readonly string memberName;

        public StaticSetter(string memberName)
        {
            this.memberName = memberName;
        }

        public void Value(object value)
        {
            FieldPropertyWrite.StaticValue<T>(memberName, value);
        }
    }
}
