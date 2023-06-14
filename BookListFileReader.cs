using System.Text.Json;

namespace Biblioteka
{
    internal class BookListFileReader : IFileObjectsReader<List<Book>>
    {
        private readonly string _fileName;

        public BookListFileReader(string fileName)
        {
            this._fileName = fileName;
        }

        public List<Book> Read()
        {
            try
            {
                List<Book> books = new();
                using StreamReader sr = new(_fileName);
                var file = sr.ReadToEnd();
                if (file != null)
                {
                    try
                    {
                        List<Book>? temporaryBooks = JsonSerializer.Deserialize<List<Book>>(file);
                        if (temporaryBooks != null)
                        {
                            books.AddRange(temporaryBooks);
                        }
                    }
                    catch (JsonException e)
                    {
                    }
                }
                return books;
            }
            catch (Exception e)
            {
                throw new IOException("Problem with id file", e);
            }
        }
    }
}
