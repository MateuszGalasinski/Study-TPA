﻿using System.Collections.Generic;

namespace Core.Model
{
    public class NamespaceMetadata : BaseMetadata
    {
        public IEnumerable<TypeMetadata> Types { get; set; }
    }
}