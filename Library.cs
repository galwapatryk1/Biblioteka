using System.Text.RegularExpressions;

namespace Biblioteka
{
    internal class Library : IStartable
    {
        private static readonly string idLocation = "C://DEV/ID.json";
        private static readonly string booksLocation = "C://DEV/books.json";
        private static readonly Regex numberRegex = new("^[0-9]+$");
        private static readonly Regex nameRegex = new("^[a-zA-Z\\s]+$");
        private static readonly Regex descriptionRegex = new("^.+$");
        private static readonly Regex menuRegex = new("^[1-6]$");

        public Menu Menu { get; set; }
        public LibraryManagement LibraryManagement { get; set; }

        public Library()
        {
            IFileObjectsReader<List<Book>> bookListFileReader = new BookListFileReader(booksLocation);
            IFileObjectsReader<ID> idFileObjectsReader = new IDFileReader(idLocation);
            ICreator<Book> bookCreator = new BookCreator(numberRegex, nameRegex, descriptionRegex);
            IGenerate fileGenerator = new FileGenerator();
            IGenerate idGenerator = new IDGenerator(fileGenerator, idFileObjectsReader);
            this.Menu = new Menu(menuRegex);
            this.LibraryManagement = new LibraryManagement(idGenerator, fileGenerator, bookListFileReader, bookCreator, booksLocation, numberRegex);
        }

        public void Start()
        {
            int value = Menu.PrintMenu();
            ProcessMenuInput(value);
        }

        public void ProcessMenuInput(int key)
        {

            switch (key)
            {
                case 1:
                    ProcessMenuInput(Menu.PrintMenu());
                    break;
                case 2:
                    LibraryManagement.ShowBooks();
                    ProcessMenuInput(Menu.PrintMenu());
                    break;
                case 3:
                    LibraryManagement.CreateBook();
                    ProcessMenuInput(Menu.PrintMenu());
                    break;
                case 4:
                    LibraryManagement.RemoveBook();
                    ProcessMenuInput(Menu.PrintMenu());
                    break;
                case 5:
                    LibraryManagement.ShowFilteredBooks();
                    ProcessMenuInput(Menu.PrintMenu());
                    break;
                case 6:
                    Console.WriteLine("Do zobaczenia wkrótce!");
                    break;
                default: throw new ArgumentException("Błąd parsowania menu.");
            }
        }
    }
}
