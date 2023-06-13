namespace Biblioteka
{
    internal interface ICreator<I>
    {
        I? Create(int id);
    }
}
