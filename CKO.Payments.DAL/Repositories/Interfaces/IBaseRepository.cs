
namespace CKO.Payments.DAL.Repositories.Interfaces
{
    internal interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        T GetById(Guid id);
        void SaveChanges();
    }
}
