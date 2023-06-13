using System.Text.Json;
using System.Text.RegularExpressions;

namespace Biblioteka
{

    internal class LibraryManagement
    {
        private static readonly List<Book> books = new();

        public string BooksLocation { get; set; }
        public IGenerate IdGenerator { get; set; }
        public IGenerate FileGenerator { get; set; }
        public IFileObjectsReader<List<Book>> BookListFileReader { get; set; }
        public ICreator<Book> BookCreator { get; set; }
        public Regex NumberRegex { get; set; }

        public LibraryManagement(
            IGenerate idGenerator,
            IGenerate fileGenerator,
            IFileObjectsReader<List<Book>> bookListFileReader,
            ICreator<Book> bookCreator,
            string booksLocation,
            Regex numberRegex)
        {
            this.FileGenerator = fileGenerator;
            this.IdGenerator = idGenerator;
            this.BookListFileReader = bookListFileReader;
            this.BookCreator = bookCreator;
            this.BooksLocation = booksLocation;
            this.NumberRegex = numberRegex;
            books.AddRange(BookListFileReader.Read());
            FileGenerator.Generate(BooksLocation);
        }

        public void ShowBooks()
        {
            Console.WriteLine("Lista książek w systemie:\n");
            string jsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(jsonString);
        }

        public void CreateBook()
        {
            Book? book = BookCreator.Create(IdGenerator.Generate());
            if (book != null)
            {
                books.Add(book);
                Console.WriteLine("Trwa zapisywanie, proszę czekać...");
                WriteToFile();
            }
        }

        public void RemoveBook()
        {
            bool continueAction = true;
            ShowBooks();
            Console.WriteLine("Wprowadź id książki do usunięcia.");
            string? idToRemove = Console.ReadLine();
            while (idToRemove == null || !NumberRegex.IsMatch(idToRemove))
            {
                Console.WriteLine("Podana wartość nie jest wartością numeryczną!");
                Console.WriteLine("Chcesz kontynuować? y/n");

                string? input = Console.ReadLine();
                if (input == null || input != "y")
                {
                    Console.WriteLine("Kontynuujmy.");
                    Console.WriteLine("Wprowadź id książki do usunięcia.");
                }
                else
                {
                    continueAction = false;
                    break;
                }

                idToRemove = Console.ReadLine();
            }
            if (continueAction)
            {
                int id = int.Parse(idToRemove);
                var lin = from bookInst in books where bookInst.ID == id select bookInst;
                if (!lin.Any())
                {
                    Console.WriteLine($"Brak książki o podanym id={idToRemove}");
                }
                else
                {
                    Console.WriteLine("Trwa usuwanie, proszę czekać...");
                    lin.ToList().ForEach(x => books.Remove(x));
                    WriteToFile();
                }
            }
        }

        public void ShowFilteredBooks()
        {
            Console.WriteLine("Podaj frazę po której chcesz przefiltrować książki:");
            string? value = Console.ReadLine();
            bool continueAction = true;
            while (value == null)
            {
                Console.WriteLine("Fraza nie może być pusta.");
                Console.WriteLine("Chcesz kontynuować? y/n");

                string? input = Console.ReadLine();
                if (input == null || input != "y")
                {
                    Console.WriteLine("Kontynuujmy.");
                    Console.WriteLine("Podaj frazę po której chcesz przefiltrować książki:");
                }
                else
                {
                    continueAction = false;
                    break;
                }

            }
            if (continueAction)
            {
                var linQueryTitle = from book in books where book.Title.Contains(value) select book;
                var linQueryDescription = from book in books where book.Description.Contains(value) select book;
                var linQueryAuthorName = from book in books where book.Author.Name.Contains(value) select book;
                var linQueryAuthorSurname = from book in books where book.Author.Surname.Contains(value) select book;
                var linQueryAuthorDescription = from book in books where book.Author.Description.Contains(value) select book;
                List<Book> filteredBooks = new(linQueryTitle);
                filteredBooks.AddRange(linQueryDescription);
                filteredBooks.AddRange(linQueryAuthorName);
                filteredBooks.AddRange(linQueryAuthorSurname);
                filteredBooks.AddRange(linQueryAuthorDescription);
                HashSet<Book> uniqueBooks = new(filteredBooks);
                string formattedText = JsonSerializer.Serialize(uniqueBooks, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(formattedText);
            }
        }

        private void WriteToFile()
        {
            string jsonString = JsonSerializer.Serialize(books);

            File.WriteAllText(BooksLocation, String.Empty);
            File.WriteAllText(BooksLocation, jsonString);
        }
    }
}
