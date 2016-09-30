using System.IO;

namespace kenobi.TripsExtension.TestDataProvider.DataProviders
{
    internal class JsonFileReader
    {
        public string GetResourceTextFile(string filename)
        {
            using (var stream = GetType().Assembly.GetManifestResourceStream(filename))
            {
                return stream != null ? GetStreamString(stream) : string.Empty;
            }
        }

        private static string GetStreamString(Stream stream)
        {
            string result;
            using (var sr = new StreamReader(stream))
                result = sr.ReadToEnd();
            return result;
        }
    }
}