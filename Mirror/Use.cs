using Mirror.Wrappers;

namespace Mirror
{
    public static class Use
    {
        public static InstanceTarget<T> Target<T>(T targetObject)
        {
            return new InstanceTarget<T>(targetObject);
        }

        public static TypeTarget<T> Target<T>()
        {
            return new TypeTarget<T> { };
        }
    }
}
