using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace SharedUILogic.Tests.Given_MainViewModel.When_GetFilePathCommand
{
    public class WhenGetFilePath : GivenMainViewModel
    {
        private string _filePath;

        public void When_ExecuteCommand()
        {
            try
            {
                Task.Run(() => _context.GetFilePathCommand.Execute(null)).Wait();
                _filePath = _context.FilePath;
            }
            catch (AggregateException)
            {
            }
        }

        [Test]
        public void And_FilePathGetter()
        {
            With_FilePathGetter();

            When_ExecuteCommand();

            Then_FilePathShouldBe(FilePath);
        }

        private void Then_FilePathShouldBe(string filePath)
        {
            _filePath.Should().Be(filePath);
        }
    }
}
