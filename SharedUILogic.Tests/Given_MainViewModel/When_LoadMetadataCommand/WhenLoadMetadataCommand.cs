using FluentAssertions;
using NUnit.Framework;
using SharedUILogic.Base;
using SharedUILogic.Model;
using System;
using System.Linq;
using System.Reflection;

namespace SharedUILogic.Tests.Given_MainViewModel.When_LoadMetadataCommand
{
    class WhenLoadMetadataCommand : GivenMainViewModel
    {
        public void When_ExecuteCommand()
        {
            try
            {
                (_context.LoadMetadataCommand as IAsyncCommand).ExecuteAsync().Wait();
            }
            catch (AggregateException)
            {
            }
        }

        [Test]
        public void And_StoreProvider()
        {
            With_TreeRoot();
            With_AssemblyMetadataStore();
            With_StoreProvider();
            With_Mapper();

            _context.FilePath = GetAssemblyPath();

            When_ExecuteCommand();

            Then_TreeRootShouldBe(TreeRoot);
        }

        private void Then_TreeRootShouldBe(TreeItem treeRoot)
        {
            _context.TreeItems.First().Should().BeEquivalentTo(treeRoot);
        }

        // to get any file that exists
        private string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return path;
        }
    }
}
