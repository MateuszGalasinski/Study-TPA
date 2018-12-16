﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace ReflectionLoading.Models
{
    public class NamespaceModel : BaseNamespaceModel
    {
        public NamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = new List<BaseTypeModel>();

            foreach (var type in types.OrderBy(t => t.Name))
            {
                Types.Add(new TypeModel(type));
            }
        }

        public override bool Equals(object obj)
        {
            var model = obj as NamespaceModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(Types, model.Types);
        }
    }
}
