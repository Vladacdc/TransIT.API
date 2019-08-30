using System.IO;

namespace TransIT.DAL.FileStorage
{
    internal class LocalStorageOptions
    {
        private string _folderPath;
        public string FolderPath
        {
            get => _folderPath;
            set
            {
                Directory.CreateDirectory(value);
                _folderPath = value;
            }
        }
    }
}
