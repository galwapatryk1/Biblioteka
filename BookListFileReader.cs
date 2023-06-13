using System.Text.Json;

namespace Biblioteka
{
    internal class BookListFileReader : IFileObjectsReader<List<Book>>
    {
        public string FileName { get; set; }

        public BookListFileReader(string fileName)
        {
            FileName = fileName;
        }

        public List<Book> Read()
        {
            try
            {
                List<Book> books = new();
                using StreamReader sr = new(FileName);
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
