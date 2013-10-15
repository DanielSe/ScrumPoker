using System.Collections.Generic;
using System.Linq;

namespace ScrumPoker.Code
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