using Microsoft.Win32;
using UILogic.Interfaces;

namespace WindowUI
{
    internal class FileDialog : IFilePathGetter
    {
        public string GetFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string result;
            if (fileDialog.ShowDialog() == true)
            {
               result = fileDialog.FileName;
            }
            else
            {
                result = string.Empty;
            }

            return result;
        }
    }
}