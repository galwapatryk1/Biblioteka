using System.Text.Json;

namespace Biblioteka
{
    internal class IDFileReader : IFileObjectsReader<ID>
    {
        public string FileName { get; set; }

        public IDFileReader(string fileName)
        {
            FileName = fileName;
        }

        public ID Read()
        {
            try
            {
                ID? id;
                using StreamReader sr = new(FileName);
                var file = sr.ReadToEnd();
                if (file != null)
                {
                    try
                    {
                        id = JsonSerializer.Deserialize<ID>(file);
                    }
                    catch (JsonException)
                    {
                        id = new(0, DateTimeOffset.Now);
                    }
                }
                else
                {
                    id = new(0, DateTimeOffset.Now);
                }
                return id;
            }
            catch (Exception e)
            {
                throw new IOException("Problem with id file", e);
            }
        }
    }
}
