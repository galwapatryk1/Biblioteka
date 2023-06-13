namespace Biblioteka
{
    internal class ID
    {
        public int Value { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public ID(int value, DateTimeOffset lastModified)
        {
            Value = value;
            LastModified = lastModified;
        }
    }
}
