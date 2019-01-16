using System.Collections.Generic;
using System.Linq;
using BaseCore.Enums;
using BaseCore.Model;

namespace DbRepository.Models
{
    public class MethodDbSaver
    {
        private MethodDbSaver()
        {
            TypeConstructors = new HashSet<TypeDbSaver>();
            TypeMethods = new HashSet<TypeDbSaver>();
        }

        public int MethodDbSaverId { get; set; }


        public MethodDbSaver(MethodBase baseMethod)
        {

            this.Name = baseMethod.Name;
            this.IsAbstract = baseMethod.IsAbstract;
            this.Accessibility = baseMethod.Accessibility;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeDbSaver.GetOrAdd(baseMethod.ReturnType);
            this.IsStatic = baseMethod.IsStatic;
            this.IsVirtual = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(TypeDbSaver.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterDbSaver(t)).ToList();
        }

        public string Name { get; set; }

        public List<TypeDbSaver> GenericArguments { get; set; }

        public Accessibility Accessibility { get; set; }


        public IsAbstract IsAbstract { get; set; }


        public IsStatic IsStatic { get; set; }


        public IsVirtual IsVirtual { get; set; }


        public TypeDbSaver ReturnType { get; set; }


        public bool Extension { get; set; }


        public List<ParameterDbSaver> Parameters { get; set; }

        public ICollection<TypeDbSaver> TypeConstructors { get; set; }

        public ICollection<TypeDbSaver> TypeMethods { get; set; }
    }
}
