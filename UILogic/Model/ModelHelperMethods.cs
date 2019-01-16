using Logic.Models;

namespace UILogic.Model
{
    public static class ModelHelperMethods
    {
        public static void CheckOrAdd(TypeModel type)
        {
            if (!TypeModel.TypeDictionary.ContainsKey(type.Name))
            {
                TypeModel.TypeDictionary.Add(type.Name, type);
            }
        }
    }
}
