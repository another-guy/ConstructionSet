using System;
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
        
        public MethodInvoke.Result ToCall(string methodName, params object[] parameters)
        {
            return MethodInvoke.StaticMethod<T>(methodName, parameters);
        }
    }
}
