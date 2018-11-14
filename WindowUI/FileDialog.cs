using Core.Components;
using Microsoft.Win32;
using UILogic.Interfaces;

namespace WindowUI
{
    internal class FileDialog : IFilePathGetter
    {
        private ILogger _logger;

        public FileDialog(ILogger logger)
        {
            _logger = logger;
        }

        public string GetFilePath()
        {
            _logger.Trace("Opening file dialog to ask for file.");
            OpenFileDialog fileDialog = new OpenFileDialog();

            string result;
            if (fileDialog.ShowDialog() == true)
            {
               result = fileDialog.FileName;
                _logger.Trace("Chosen file: " + result);
            }
            else
            {
                result = string.Empty;
                _logger.Trace("No file has been chosen");
            }

            return result;
        }
    }
}