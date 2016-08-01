using Mirror.Internals;

namespace Mirror.Wrappers
{
    public class InstanceTarget<T>
    {
        private T @object;

        public InstanceTarget(T targetObject)
        {
            @object = targetObject;
        }

        public InstanceSetter<T> ToSet(string memberName)
        {
            return new InstanceSetter<T>(@object, memberName);
        }

        public R ToGet<R>(string memberName)
        {
            return (R)FieldPropertyRead.Value(@object, memberName);
        }
    }
}
