using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data2.Repositories
{
    public  interface IRepository<TEntity> where TEntity : class
    {
        // Generic bir Interface
        // Where TEntity : class -> TEntity yerine class yazılabilir.
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);
        TEntity GetById(int id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    }
}
