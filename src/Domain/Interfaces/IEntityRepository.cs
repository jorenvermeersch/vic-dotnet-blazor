namespace Domain.Interfaces
{
    public interface IEntityRepository<T> where T : Entity
    {
        #region Fields
        public ISet<T> Entities { get; }
        #endregion

        #region Methods
        public void Add(T entity);

        public T GetById(long id);

        public void UpdateById(long id, T entity);

        public void DeleteById(long id);
        #endregion
    }
}
