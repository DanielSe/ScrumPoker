namespace ScrumPoker.Models
{
    public interface IIdGenerator<TKey>
    {
        TKey CreateId();
    }
}