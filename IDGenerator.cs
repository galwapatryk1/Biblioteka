using System.Text;
using System.Text.Json;
namespace Biblioteka
{
    internal class IDGenerator : IGenerate
    {
        private readonly IGenerate _fileGenerator;
        private readonly IFileObjectsReader<ID> _fileObjectsReader;
        private readonly string _fileName;
        
        private static ID? id;

        public IDGenerator(IGenerate fileGenerator, IFileObjectsReader<ID> objectsReader, string filename)
        {
            this._fileGenerator = fileGenerator;
            this._fileObjectsReader = objectsReader;
            this._fileName = filename;
            _fileGenerator.Generate(_fileName);
            id = _fileObjectsReader.Read();
        }

        public int Generate()
        {
            int value = id.Value;
            value++;
            id.Value = value;
            id.LastModified = DateTimeOffset.Now;
            WriteToFile();
            return value;
        }

        private static void WriteToFile()
        {
            string jsonString = JsonSerializer.Serialize(id);

            File.WriteAllText(_fileName, String.Empty);
            File.WriteAllText(_fileName, jsonString);
        }

        public void Generate(string name)
        {
            throw new NotImplementedException();
        }
    }
}