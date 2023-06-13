namespace Biblioteka
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IStartable startable = new Library();
            startable.Start();
        }
    }
}
