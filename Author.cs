namespace Biblioteka
{
    internal class Author
    {
        public string Name { get; }
        public string Surname { get; }
        public string Description { get; }

        public Author(string name, string surname, string description)
        {
            Name = name;
            Surname = surname;
            Description = description;
        }
    }
}
