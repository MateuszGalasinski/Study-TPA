using BaseCore.Enums;
using System.Collections.Generic;

namespace BaseCore.Model
{
    public class MethodBase
    {
        public string Name { get; set; }

        public List<TypeBase> GenericArguments { get; set; }

        public Accessibility Accessibility { get; set; }

        public IsAbstract IsAbstract { get; set; }

        public IsStatic IsStatic { get; set; }

        public IsVirtual VirtualEnum { get; set; }

        public TypeBase ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterBase> Parameters { get; set; }
    }
}
