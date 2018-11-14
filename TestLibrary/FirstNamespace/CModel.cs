namespace TestLibrary.FirstNamespace
{
    public class CModel : BaseModel
    {
        public override string AbstractMethod()
        {
            throw new System.NotImplementedException();
        }

        public override string VirtualMethod()
        {
            return base.VirtualMethod();
        }
    }
}
