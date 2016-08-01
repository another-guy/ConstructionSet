using System;

namespace Mirror.Tests.TestClasses
{
    public class ClassWithPrivateMethods
    {
        private void InstanceVoidMethodWithArgs(ITrackable trackable)
        {
            trackable.Touch();
        }

        private string InstanceStringMethodWithArgs(ITrackable trackable)
        {
            trackable.Touch();
            return "success";
        }

        private void InstanceVoidMethodWithoutArgs()
        {
        }

        private string InstanceStringMethodWithoutArgs()
        {
            return "successInstanceStringMethodWithoutArgs";
        }

        private static void StaticVoidMethodWithArgs(ITrackable trackable)
        {
            trackable.Touch();
        }

        private static string StaticStringMethodWithArgs(ITrackable trackable)
        {
            trackable.Touch();
            return "successStaticStringMethodWithArgs";
        }

        private static void StaticVoidMethodWithoutArgs()
        {
        }

        private static string StaticStringMethodWithoutArgs()
        {
            return "successStaticStringMethodWithoutArgs";
        }
    }

    public interface ITrackable
    {
        void Touch();
    }
}
