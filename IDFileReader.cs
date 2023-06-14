using System.Text.Json;

namespace Biblioteka
{
    internal class IDFileReader : IFileObjectsReader<ID>
    {
        private readonly string _fileName;

        public IDFileReader(string fileName)
        {
            this._fileName = fileName;
        }

        public ID Read()
        {
            try
            {
                ID? id;
                using StreamReader sr = new(_fileName);
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
