using System.IO;
using System.Linq;
using System.Reflection;

namespace pythonnet
{
    public static class EmbeddedStore
    {
        public static string[] ListAllFiles()
        {
            return StorageAssembly.GetManifestResourceNames();
        }

        public static string FindResourceName(string filename)
        {
            var matches = StorageAssembly.GetManifestResourceNames()
                .Where(resourceName => resourceName.Contains(filename)).ToArray();
            return matches.Length > 0 ? matches[0] : string.Empty;
        }

        public static Stream ReadFile(string filename)
        {
            return StorageAssembly.GetManifestResourceStream(FindResourceName(filename));
        }

        public static string ReadFileAsText(string filename)
        {
            using var stream = ReadFile(filename);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static readonly Assembly StorageAssembly = typeof(EmbeddedStore).Assembly;
    }
}