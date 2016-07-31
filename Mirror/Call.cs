using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Mirror
{
    public static class Call
    {
        public struct Result
        {
            public bool HasResult;
            public object Value;
        }

        // TODO FIXME
        //public static TargetedCall<T> Target<T>(T target)
        //{
        //    return null;
        //}

        public static R InstanceMethod<T, R>(
            this T target,
            string methodName,
            params object[] parameters)
        {
            var result = InvokeMethod(false, target, methodName, parameters);
            return (R)result.Value;
        }

        public static Result InstanceMethod<T>(
            this T target,
            string methodName,
            params object[] parameters)
        {
            return InvokeMethod(false, target, methodName, parameters);
        }

        public static Result StaticMethod<T>(
            this T target,
            string methodName,
            params object[] parameters)
        {
            return InvokeMethod(true, target, methodName, parameters);
        }

        private static Result InvokeMethod<T>(
            bool @static,
            T target,
            string methodName,
            params object[] parameters)
        {
            var methods = FindMethodsByName<T>(methodName, @static: @static);

            var method = ArgumentSignatureMatcher.FindBestMatching(methods, parameters);

            return Invoke(method, target, parameters);
        }

        private static List<MethodInfo> FindMethodsByName<T>(
            string methodName,
            bool @static)
        {
            var methodKind = @static ? BindingFlags.Static : BindingFlags.Instance;
            var type = typeof(T);
            var methods = type
                .GetTypeInfo()
                .GetMethods(BindingFlags.NonPublic | methodKind)
                .Where(m => m.Name == methodName)
                .ToList();

            if (methods.Count <= 0)
            {
                var staticOrInstance = @static ? "static" : "instance";
                var message =
                    $"No {staticOrInstance} method with name {methodName} " +
                    $"was found in type {type.FullName}.";
                throw new InvalidOperationException(message);
            }

            return methods;
        }

        private static Result Invoke<T>(MethodInfo method, T target, object[] parameters)
        {
            var hasNoReturnType = method.ReturnType == typeof(void);
            if (hasNoReturnType)
            {
                method.Invoke(target, parameters);
                return new Result { HasResult = false, Value = null };
            }
            else
            {
                var result = method.Invoke(target, parameters);
                return new Result { HasResult = true, Value = result };
            }
        }
    }
}
