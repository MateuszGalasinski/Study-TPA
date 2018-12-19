using Base.Enums;
using System.Collections.Generic;

namespace Base.Model
{
    public class MethodBase
    {
        public string Name { get; set; }

        public List<TypeBase> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public VirtualEnum VirtualEnum { get; set; }

        public TypeBase ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterBase> Parameters { get; set; }
    }
}
