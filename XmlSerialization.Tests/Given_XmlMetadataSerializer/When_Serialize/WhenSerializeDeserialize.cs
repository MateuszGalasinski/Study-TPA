using FluentAssertions;
using NUnit.Framework;
using ReflectionLoading.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XmlSerialization.Tests.Given_XmlMetadataSerializer.When_Serialize
{
    public class WhenSerializeDeserialize : GivenXmlMetadataSerializer
    {
        private Task<AssemblyModel> _when;
        public void When_SerializeDeserialize(AssemblyModel model)
        {
            try
            {
                Task.Run(() => _context.Serialize(model, ResultPath)).Wait();
                _when = Task.Run(() => _context.Deserialize(ResultPath));
                _when.Wait();
            }
            catch (AggregateException)
            {
            }
        }

        [Test]
        public void WhenSerialize_OnlyAssembly()
        {
            With_FilePathFromResources();
            With_ResultFilePath("assembly.xml");
            With_AssemblyFromFilePath();

            When_SerializeDeserialize(Root);

            Result_Should_BeEquivalent(Root);
        }

        private void Result_Should_BeEquivalent(AssemblyModel root)
        {
            _when.Result.Name.Should().BeEquivalentTo(root.Name);
            _when.Result.NamespaceModels.Should().HaveSameCount(root.NamespaceModels);
            _when.Result.NamespaceModels.Should().OnlyContain(p => root.NamespaceModels.Count(n => n.Name == p.Name) == 1);
        }
    }
}
