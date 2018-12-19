using BaseCore.Model;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XmlSerialization.Tests.Given_XmlMetadataSerializer.When_Serialize
{
    public class WhenSerializeDeserialize : GivenXmlDataContractSerializer
    {
        private Task<AssemblyBase> _when;
        public void When_SerializeDeserialize(AssemblyBase model)
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

        private void Result_Should_BeEquivalent(AssemblyBase root)
        {
            _when.Result.Name.Should().BeEquivalentTo(root.Name);
            _when.Result.Namespaces.Should().HaveSameCount(root.Namespaces);
            _when.Result.Namespaces.Should().OnlyContain(p => root.Namespaces.Count(n => n.Name == p.Name) == 1);
        }
    }
}
