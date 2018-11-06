using BusinessLogic.Model;
using Core.Components;
using Core.Model;
using SharedUILogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class MetadataTreeItemMapper : IMapper<AssemblyMetadataStore, TreeItem>
    {
        public TreeItem Map(AssemblyMetadataStore objectToMap)
        {
            if (objectToMap == null)
            {
                throw new ArgumentNullException($"{nameof(objectToMap)} argument is null.");
            }

            Dictionary<string, TreeItem> instances = new Dictionary<string, TreeItem>();
            List<OneWayRelation> relations = new List<OneWayRelation>();

            // assembly
            bool hasChildren = objectToMap.AssemblyMetadata?.Namespaces.Any() == true;
            TreeItem assemblyItem = new TreeItem(objectToMap.AssemblyMetadata.Id, hasChildren);
            instances.Add(assemblyItem.Name, assemblyItem);

            ProcessNamespaceItems(objectToMap, instances, relations, assemblyItem);

            ProcessMutlipleRelationItems(objectToMap.MethodsDictionary, instances, relations, GetRelations, MapItem);
            ProcessMutlipleRelationItems(objectToMap.TypesDictionary, instances, relations, GetRelations, MapItem);
            ProcessSingleRelationItems(objectToMap.ParametersDictionary, instances, relations, GetRelation, MapItem);
            ProcessSingleRelationItems(objectToMap.PropertiesDictionary, instances, relations, GetRelation, MapItem);

            // lets get fornicating
            foreach (var relation in relations)
            {
                instances[relation.Parent].Children.Add(instances[relation.Child]);
            }

            return assemblyItem;
        }

        private void ProcessNamespaceItems(
            AssemblyMetadataStore objectToMap,
            Dictionary<string, TreeItem> instances,
            List<OneWayRelation> relations,
            TreeItem assemblyItem)
        {
            foreach (var namespaceMetadataDto in objectToMap.NamespacesDictionary)
            {
                TreeItem item = MapItem(namespaceMetadataDto.Value);
                foreach (var relation in GetRelations(namespaceMetadataDto.Value))
                {
                    relations.Add(relation);
                }

                relations.Add(new OneWayRelation(assemblyItem.Name, item.Name));
                instances.Add(item.Name, item);
            }
        }

        private void ProcessSingleRelationItems<T>(
            Dictionary<string, T> itemsDictionary,
            Dictionary<string, TreeItem> instances,
            List<OneWayRelation> relations,
            Func<T, OneWayRelation> relationFunction,
            Func<T, TreeItem> mapFunction)
        {
            foreach (var dictItem in itemsDictionary)
            {
                TreeItem item = mapFunction(dictItem.Value);
                relations.Add(relationFunction(dictItem.Value));
                instances.Add(dictItem.Key, item);
            }
        }

        private void ProcessMutlipleRelationItems<T>(
            Dictionary<string, T> itemsDictionary,
            Dictionary<string, TreeItem> instances,
            List<OneWayRelation> relations,
            Func<T, IEnumerable<OneWayRelation>> relationFunction,
            Func<T, TreeItem> mapFunction)
        {
            foreach (var dictItem in itemsDictionary)
            {
                TreeItem item = mapFunction(dictItem.Value);
                foreach (var relation in relationFunction(dictItem.Value))
                {
                    relations.Add(relation);
                }

                instances.Add(dictItem.Key, item);
            }
        }

        private OneWayRelation GetRelation(PropertyMetadata value)
        {
            return new OneWayRelation(value.Id, value.TypeMetadata.Id);
        }

        private TreeItem MapItem(PropertyMetadata value)
        {
            return new TreeItem($"Property: {value.Name}", true);
        }

        private OneWayRelation GetRelation(ParameterMetadata value)
        {
            return new OneWayRelation(value.Id, value.TypeMetadata.Id);
        }

        private TreeItem MapItem(ParameterMetadata value)
        {
            return new TreeItem($"Parameter: {value.Name}", true);
        }

        private IEnumerable<OneWayRelation> GetRelations(NamespaceMetadata value)
        {
            foreach (var item in value.Types)
            {
                yield return new OneWayRelation($"Namespace: {value.Id}", item.Id);
            }
        }

        private TreeItem MapItem(NamespaceMetadata value)
        {
            return new TreeItem($"Namespace: {value.Id}", value.Types.Any());
        }

        private IEnumerable<OneWayRelation> GetRelations(TypeMetadata value)
        {
            foreach (var item in value.Constructors)
            {
                yield return new OneWayRelation(value.Id, item.Id);
            }

            foreach (var item in value.Methods)
            {
                yield return new OneWayRelation(value.Id, item.Id);
            }

            foreach (var item in value.Properties)
            {
                yield return new OneWayRelation(value.Id, item.Id);
            }

            // foreach (var item in value.Attributes)
            // {
            //    yield return new Relation(value.Id, );
            // }
            // TODO check attributes

            foreach (var item in value.GenericArguments)
            {
                yield return new OneWayRelation(value.Id, item.Id);
            }

            foreach (var item in value.ImplementedInterfaces)
            {
                yield return new OneWayRelation(value.Id, item.Id);
            }

            foreach (var item in value.NestedTypes)
            {
                yield return new OneWayRelation(value.Id, item.Id);
            }
        }

        // TODO: handle attributes impact on visibility
        private TreeItem MapItem(TypeMetadata objectToMap)
        {
            return new TreeItem(
                $"{objectToMap.Kind.ToString().Replace("Type", string.Empty)}: {objectToMap.Name}",
                objectToMap.BaseType != null
                || objectToMap.DeclaringType != null
                || objectToMap.Constructors?.Any() == true
                || objectToMap.Methods?.Any() == true
                || objectToMap.GenericArguments?.Any() == true
                || objectToMap.ImplementedInterfaces?.Any() == true
                || objectToMap.NestedTypes?.Any() == true
                || objectToMap.Properties?.Any() == true);
        }

        private IEnumerable<OneWayRelation> GetRelations(MethodMetadata parent)
        {
            foreach (var argument in parent.GenericArguments)
            {
                yield return new OneWayRelation(parent.Id, argument.Id);
            }

            foreach (var parameter in parent.Parameters)
            {
                yield return new OneWayRelation(parent.Id, parameter.Id);
            }

            if (parent.ReturnType != null) yield return new OneWayRelation(parent.Id, parent.ReturnType.Id);
        }

        private TreeItem MapItem(MethodMetadata objectToMap)
        {
            bool hasChildren =
                objectToMap.GenericArguments.Any() ||
                objectToMap.Parameters.Any();

            return new TreeItem(
                $"{objectToMap.Modifiers.Item1} " +
                $"{objectToMap.ReturnType?.Name ?? "void"} " +
                $"{objectToMap.Name}",
                hasChildren);
        }
    }
}