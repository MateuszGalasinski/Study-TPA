namespace TestLibrary.FirstNamespace
{
    public abstract class BaseModel
    {
        public abstract string AbstractMethod();

        public virtual string VirtualMethod()
        {
            return string.Empty;
        }
    }
}
