using Mirror.Internals;

namespace Mirror.Wrappers
{
    public class TypeTarget<T>
    {
        public T ToCreateInstance(params object[] parameters)
        {
            return ConstructorInvoke<T>.UsingPrivateConstructor(parameters);
        }

        public StaticSetter<T> ToSet(string memberName)
        {
            return new StaticSetter<T>(memberName);
        }

        public R ToGet<R>(string memberName)
        {
            return (R)FieldPropertyRead.StaticValue<T>(memberName);
        }
        
        public void ToCall(string methodName)
        {
            MethodInvoke.StaticMethod<T>(methodName, new object[0]);
        }

        public void ToCall(
            string methodName,
            object first,
            params object[] rest)
        {
            var parameters = MethodInvoke.NormalizeParameters(first, rest);
            MethodInvoke.StaticMethod<T>(methodName, parameters);
        }

        public R ToCall<R>(
            string methodName)
        {
            return (R)MethodInvoke.StaticMethod<T>(methodName, new object[0]).Value;
        }

        public R ToCall<R>(
            string methodName,
            object first,
            params object[] rest)
        {
            var parameters = MethodInvoke.NormalizeParameters(first, rest);
            return (R)MethodInvoke.StaticMethod<T>(methodName, parameters).Value;
        }
    }
}
