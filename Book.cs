namespace Biblioteka
{
    internal class Book
    {
        public int ID { get; }
        public string Title { get; }
        public string Description { get; }
        public Author Author { get; }
        public int Size { get; }

        public Book(int iD, string title, string description, Author author, int size)
        {
            ID = iD;
            Title = title;
            Description = description;
            Author = author;
            Size = size;
        }
    }
}
