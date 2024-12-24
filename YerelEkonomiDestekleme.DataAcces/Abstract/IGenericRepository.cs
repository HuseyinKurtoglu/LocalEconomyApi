using System.Linq.Expressions;

namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
