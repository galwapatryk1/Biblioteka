namespace Biblioteka
{
    internal class FileGenerator : IGenerate
    {

        public void Generate(string name)
        {
            Boolean exist = File.Exists(name);
            if (!exist)
            {
                File.Create(name).Close();
            }
        }

        public int Generate()
        {
            throw new NotImplementedException();
        }
    }
}
