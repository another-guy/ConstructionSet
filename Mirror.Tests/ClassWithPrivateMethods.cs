using System;

namespace Mirror.Tests
{
    public class ClassWithPrivateMethods
    {
        public static ITrackable GlobalTrackable;

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
            GlobalTrackable.Touch();
        }

        private string InstanceStringMethodWithoutArgs()
        {
            GlobalTrackable.Touch();
            return "success";
        }
        private static void StaticVoidMethodWithArgs(ITrackable trackable)
        {
            trackable.Touch();
        }

        private static string StaticStringMethodWithArgs(ITrackable trackable)
        {
            trackable.Touch();
            return "success";
        }

        private static void StaticVoidMethodWithoutArgs()
        {
            GlobalTrackable.Touch();
        }

        private static string StaticStringMethodWithoutArgs()
        {
            GlobalTrackable.Touch();
            return "success";
        }
    }

    public interface ITrackable
    {
        void Touch();
    }
}
