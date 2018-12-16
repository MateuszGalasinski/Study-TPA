using System.Collections.Generic;
using Core.Constants;

namespace Core.Model
{
    public abstract class BaseMethodModel
    {
    
        public string Name { get; set; }

        public List<BaseTypeModel> GenericArguments { get; set; }

        public Accessibility Accessibility { get; set; }

        public IsSealed IsSealed { get; set; }

        public IsAbstract IsAbstract { get; set; }

        public IsStatic IsStatic { get; set; }

        public IsVirtual IsVirtual { get; set; }

        public BaseTypeModel ReturnType { get; set; }

        public bool Extension { get; set; }
        public List<BaseParameterModel> Parameters { get; set; }


        public override bool Equals(object obj)
        {
            var model = obj as BaseMethodModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   IsVirtual == model.IsVirtual &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(ReturnType, model.ReturnType) &&
                   Extension == model.Extension &&
                   EqualityComparer<List<BaseParameterModel>>.Default.Equals(Parameters, model.Parameters);
        }
    }
}
