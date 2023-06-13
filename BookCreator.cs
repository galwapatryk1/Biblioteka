using System.Text.RegularExpressions;

namespace Biblioteka
{
    internal class BookCreator : ICreator<Book>
    {

        public Regex BookCountRegex { get; set; }
        public Regex NameRegex { get; set; }
        public Regex DescriptionRegex { get; set; }

        public BookCreator(Regex bookCountRegex, Regex nameRegex, Regex descriptionRegex)
        {
            this.BookCountRegex = bookCountRegex;
            this.NameRegex = nameRegex;
            this.DescriptionRegex = descriptionRegex;
        }

        public Book? Create(int id)
        {
            Console.WriteLine("Podaj tytuł książki");
            string? title = VariableLoop("Tytuł nie może być pusty!", DescriptionRegex);
            if (title == null) return null;

            Console.WriteLine("Podaj opis książki");
            string? description = VariableLoop("Opis nie może być pusty!", DescriptionRegex);
            if (description == null) return null;

            Console.WriteLine("Podaj imię autora książki");
            string? authorName = VariableLoop("Imię autora nie może posiadać innych znaków niż litery i spacje!", NameRegex);
            if (authorName == null) return null;

            Console.WriteLine("Podaj nazwisko autora książki");
            string? authorSurname = VariableLoop("Nazwisko autora nie może posiadać innych znaków niż litery i spacje!", NameRegex);
            if (authorSurname == null) return null;

            Console.WriteLine("Podaj opis autora książki");
            string? authorDescription = VariableLoop("Opis autora nie może być pusty!", DescriptionRegex);
            if (authorDescription == null) return null;

            Console.WriteLine("Podaj ilość książek");
            string? sizeOfBooks = VariableLoop("Podana wartość nie jest wartością numeryczną!", BookCountRegex);
            if (sizeOfBooks == null) return null;

            int size = int.Parse(sizeOfBooks);
            Author author = new(authorName, authorSurname, authorDescription);
            return new(id, title, description, author, size);
        }

        private static string VariableLoop(string info, Regex regex)
        {
            string? variable = Console.ReadLine();
            while (variable == null || !regex.IsMatch(variable))
            {
                variable = null;
                Console.WriteLine(info);
                bool continuation = CheckContinuation();
                if (!continuation) break;
                variable = Console.ReadLine();
            }
            return variable;
        }

        private static bool CheckContinuation()
        {
            Console.WriteLine("Chcesz kontynuować? y/n");

            string? input = Console.ReadLine();
            if (input == null || input != "y")
            {
                Console.WriteLine("Ok.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
