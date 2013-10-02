using System.Collections.Generic;

namespace ScrumPoker.Models
{
    public interface ICrud<TEntity, in TKey>
        where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity Read(TKey key);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);

        IEnumerable<TEntity> List();
    }
}