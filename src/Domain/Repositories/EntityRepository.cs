﻿using Domain.Interfaces;

namespace Domain.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : Entity
    {
        #region Fields
        private readonly ISet<T> _entities;
        #endregion

        #region Properties
        public ISet<T> Entities => _entities;
        #endregion

        #region Constructors
        public EntityRepository()
        {
            _entities = new HashSet<T>();
        }
        #endregion

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void DeleteById(long id)
        {
            T entity = GetById(id);
            _entities.Remove(entity);
        }

        public T GetById(long id)
        {
            return _entities.First(entity => entity.Id == id);
        }

        public void UpdateById(long id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
