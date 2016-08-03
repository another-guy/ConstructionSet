using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Mirror.Internals
{
    public static class MethodInvoke
    {
        // TODO Delete this!
        public struct Result
        {
            public bool HasResult;
            public object Value;
        }

        public static object[] NormalizeParameters(object first, object[] rest)
        {
            return new object[] { first }.Concat(rest).ToArray();
        }

        public static Result InstanceMethod<T>(
            this T target,
            string methodName,
            object[] parameters)
        {
            return InvokeMethod<T>(false, target, methodName, parameters);
        }

        public static Result StaticMethod<T>(
            string methodName,
            object[] parameters)
        {
            return InvokeMethod<T>(true, null, methodName, parameters);
        }

        private static Result InvokeMethod<T>(
            bool @static,
            object target,
            string methodName,
            params object[] parameters)
        {
            var methods = FindMethodsByName<T>(methodName, @static: @static);

            var method = ArgumentSignatureMatcher.FindBestMatching(methods, parameters);

            return Invoke(@static, method, target, parameters);
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

        private static Result Invoke(bool @static, MethodInfo method, object target, object[] parameters)
        {
            var hasNoReturnType = method.ReturnType == typeof(void);
            if (hasNoReturnType)
            {
                if (@static)
                    method.Invoke(null, parameters);
                else
                    method.Invoke(target, parameters);
                return new Result { HasResult = false, Value = null };
            }
            else
            {
                var result = @static ?
                    method.Invoke(null, parameters) :
                    method.Invoke(target, parameters);
                return new Result { HasResult = true, Value = result };
            }
        }
    }
}
