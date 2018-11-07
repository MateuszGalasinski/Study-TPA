using Core.Model;
using FluentAssertions;
using NUnit.Framework;
using SharedUILogic.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedUILogic.Tests.Given_MetadataTreeItemMapper.When_Map
{
    public class WhenMap : GivenMetadataItemMapper
    {
        private TreeItem _rootItem;

        public void When_MapStorage()
        {
            try
            {
                Task.Run(() => { _rootItem = _context.Map(_storage); }).Wait();
            }
            catch (AggregateException)
            {
            }
        }

        [Test]
        public void And_OnlyAssembly()
        {
            With_AssemblyMetadata();

            When_MapStorage();

            Then_TreeShouldBe(new TreeItem(_assemblyName, false));
        }

        [Test]
        public void And_AssemblyWithEmptyNamespace()
        {
            With_AssemblyMetadata();
            With_NamespaceMetaData();

            When_MapStorage();

            Then_TreeShouldBe(new TreeItem(_assemblyName, true)
            {
                Children = { new TreeItem($"Namespace: {_namespaceName}", false)}
            });
        }

        [Test]
        public void And_AssemblyWithNamespaceAndType()
        {
            With_AssemblyMetadata();
            With_NamespaceMetaData();
            With_TypeMetaData();

            When_MapStorage();

            Then_TreeShouldBe(new TreeItem(_assemblyName, true)
            {
                Children =
                {
                    new TreeItem($"Namespace: {_namespaceName}", true)
                    {
                        Children = { new TreeItem($"Enum: {_typeName}", false)}
                    }
                }
            });
        }

        [Test]
        public void And_AssemblyWithNamespaceAndTypeWithProperty()
        {
            With_AssemblyMetadata();
            With_NamespaceMetaData();
            With_TypeMetaData();
            With_PropertyMetadata();

            When_MapStorage();

            Then_TreeShouldBe(new TreeItem(_assemblyName, true)
            {
                Children =
                {
                    new TreeItem($"Namespace: {_namespaceName}", true)
                    {
                        Children =
                        {
                            new TreeItem($"Class: {_typeName}", true)
                            {
                                Children =
                                {
                                    new TreeItem($"Property: {_propertyName}", true)
                                    {
                                        Children = { new TreeItem($"Enum: {_secondTypeName}", false)}
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        [Test]
        public void And_AssemblyWithNamespace_AndTypeWithVoidMethod_WithNoParameters()
        {
            With_AssemblyMetadata();
            With_NamespaceMetaData();
            With_TypeMetaData();
            With_VoidMethodMetadata(new List<ParameterMetadata>());

            When_MapStorage();

            Then_TreeShouldBe(new TreeItem(_assemblyName, true)
            {
                Children =
                {
                    new TreeItem($"Namespace: {_namespaceName}", true)
                    {
                        Children =
                        {
                            new TreeItem($"Class: {_typeName}", true)
                            {
                                Children =
                                {
                                    new TreeItem($"Public void {_methodName}", false)
                                    {
                                       
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        [Test]
        public void And_AssemblyWithNamespace_AndTypeWithVoidMethod_WithParameters()
        {
            With_AssemblyMetadata();
            With_NamespaceMetaData();
            With_TypeMetaData();
            With_VoidMethodMetadata(new List<ParameterMetadata>()
            {
                new ParameterMetadata(_parameterName, CreateSimpleTypeMetadata(_thirdTypeName))
                {
                    Id = _parameterName
                }
            });

            When_MapStorage();

            Then_TreeShouldBe(new TreeItem(_assemblyName, true)
            {
                Children =
                {
                    new TreeItem($"Namespace: {_namespaceName}", true)
                    {
                        Children =
                        {
                            new TreeItem($"Class: {_typeName}", true)
                            {
                                Children =
                                {
                                    new TreeItem($"Public void {_methodName}", true)
                                    {
                                        Children =
                                        {
                                            new TreeItem($"Parameter: {_parameterName}", true)
                                            {
                                                Children =
                                                {
                                                    new TreeItem($"Enum: {_thirdTypeName}", false)
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        public void Then_TreeShouldBe(TreeItem correctRoot)
        {
            _rootItem.Should().BeEquivalentTo(correctRoot);
        }
    }
}
