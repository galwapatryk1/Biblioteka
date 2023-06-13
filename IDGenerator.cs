using System.Text;
using System.Text.Json;
namespace Biblioteka
{
    internal class IDGenerator : IGenerate
    {
        private static readonly string idLocation = "C://DEV/ID.json";
        public IGenerate fileGenerator { get; set; }
        public IFileObjectsReader<ID> fileObjectsReader { get; set; }
        
        private static ID? id;

        public IDGenerator(IGenerate fileGenerator, IFileObjectsReader<ID> objectsReader)
        {
            this.fileGenerator = fileGenerator;
            this.fileObjectsReader = objectsReader;
            fileGenerator.Generate(idLocation);
            id = objectsReader.Read();
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

            File.WriteAllText(idLocation, String.Empty);
            File.WriteAllText(idLocation, jsonString);
        }

        public void Generate(string name)
        {
            throw new NotImplementedException();
        }
    }
}