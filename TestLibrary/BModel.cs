using System;

namespace TestLibrary
{
    public class BModel : IComparable
    {
        public string Field1;
        public int Field2;
        public object Field3;
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
