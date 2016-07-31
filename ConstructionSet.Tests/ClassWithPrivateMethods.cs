using System;

namespace ConstructionSet.Tests
{
    public class ClassWithPrivateMethods
    {
        public static ITrackable GlobalTrackable;

        private void Method1(ITrackable trackable)
        {
            trackable.Touch();
        }

        private string Method2(ITrackable trackable)
        {
            trackable.Touch();
            return "success";
        }
        private void Method3()
        {
            GlobalTrackable.Touch();
        }

        private string Method4()
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
