using System.IO;
using System.Reflection;

namespace AWS.S3
{
    public static class FileHelper
    {
        public static Stream GetFileStream(string fileName)
        {
            var resource = string.Format("AWS.S3.TestFiles.{0}", fileName);
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
        }
    }
}
