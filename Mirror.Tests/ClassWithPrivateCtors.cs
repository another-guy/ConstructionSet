namespace Mirror.Tests
{
    public class ClassWithPrivateCtors
    {
        public string S { get; set; }
        public object O { get; set; }

        // Default ctor
        private ClassWithPrivateCtors()
        {
        }

        // Single arg ctor
        private ClassWithPrivateCtors(string s)
        {
            S = s;
        }

        private ClassWithPrivateCtors(object o)
        {
            O = o;
        }

        private ClassWithPrivateCtors(Parent p)
        {
        }

        // Ctors with two args and max scores >= 2000
        private ClassWithPrivateCtors(int i, string s)
        {
            S = s;
        }

        private ClassWithPrivateCtors(int i, object o)
        {
            O = o;
        }

        private ClassWithPrivateCtors(int i, Parent p)
        {
        }

        // Clashing ctors
        private ClassWithPrivateCtors(object o, int i1, int i2)
        {
        }

        private ClassWithPrivateCtors(int i1, int i2, object o)
        {
        }
    }

    public class Parent { }

    public class Child : Parent { }
}
