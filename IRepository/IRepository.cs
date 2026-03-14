namespace GestionProductosAPI.IRepository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetByID(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task save();

        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    }
}
