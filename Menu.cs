using System.Text.RegularExpressions;

namespace Biblioteka
{
    internal class Menu
    {
        private static readonly string menu = @"1. Menu
2. Spis książek
3. Dodaj książkę
4. Usuń książkę
5. Szukaj książkę
6. Wyjście";

        public Regex MenuRegex { get; set; }

        public Menu(Regex menuRegex)
        {
            this.MenuRegex = menuRegex;
        }

        public int PrintMenu()
        {
            Console.WriteLine(menu);
            String value = HandleInput();
            return int.Parse(value);
        }

        public string HandleInput()
        {
            string? value = Console.ReadLine();
            while (value == null || !MenuRegex.IsMatch(value))
            {
                Console.WriteLine("Wprowadzona opcja jest pusta bądź o błędnej wartości!");
                value = Console.ReadLine();
            }

            return value;
        }
    }
}
