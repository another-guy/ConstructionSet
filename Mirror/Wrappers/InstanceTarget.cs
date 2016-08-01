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

        public void ToCall(string methodName, params object[] parameters)
        {
            MethodInvoke.InstanceMethod(@object, methodName, parameters);
        }

        public R ToCall<R>(string methodName, params object[] parameters)
        {
            return (R)MethodInvoke.InstanceMethod(@object, methodName, parameters).Value;
        }
    }
}
