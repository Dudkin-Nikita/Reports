namespace Reports.Domain.Repositories.Abstract
{
    public interface IRepository<T>
    {
        IQueryable<T> GetEntities();
        T? GetEntityById(int id);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(int id);
    }
}
