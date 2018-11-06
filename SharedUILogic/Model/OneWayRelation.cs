namespace BusinessLogic.Model
{
    public class OneWayRelation
    {
        public OneWayRelation(string parent, string child)
        {
            Parent = parent;
            Child = child;
        }

        public string Parent { get; set; }

        public string Child { get; set; }
    }
}
