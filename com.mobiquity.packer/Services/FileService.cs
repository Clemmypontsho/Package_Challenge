using System.IO;
using System.Text;

namespace com.mobiquity.packer.Services
{
    public class FileService : IFileService
    {
        public bool IsExist(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new APIException("Error: The path cannot be empty!");
            }

            return File.Exists(path);
        }

        public string[] ReadFile(string path)
        {
            return File.ReadAllLines(path, Encoding.UTF8);
        }
    }
}