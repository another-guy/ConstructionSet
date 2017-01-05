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

        public InstanceSetter<T> Set(string memberName)
        {
            return new InstanceSetter<T>(@object, memberName);
        }

        public R ToGet<R>(string memberName)
        {
            return (R)FieldPropertyRead.Value(@object, memberName);
        }

        public void ToCall(string methodName)
        {
            MethodInvoke.InstanceMethod(@object, methodName, new object[0]);
        }

        public void ToCall(
            string methodName,
            object first,
            params object[] rest)
        {
            var parameters = MethodInvoke.NormalizeParameters(first, rest);
            MethodInvoke.InstanceMethod(@object, methodName, parameters);
        }

        public R ToCall<R>(string methodName)
        {
            return (R)MethodInvoke.InstanceMethod(@object, methodName, new object[0]).Value;
        }

        public R ToCall<R>(
            string methodName,
            object first,
            params object[] rest)
        {
            var parameters = MethodInvoke.NormalizeParameters(first, rest);
            return (R)MethodInvoke.InstanceMethod(@object, methodName, parameters).Value;
        }
    }
}
