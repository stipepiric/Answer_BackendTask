using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Core.Entities;

namespace AnswerKing.Infrastructure.Services
{
    public interface IMemory<T> where T : IEntity
    {
        public T CreateEntity<T>() where T : class, IEntity, new();
        public T CreateAndAddEntity<T>() where T : class, IEntity, new();
        public T AddEntity<T>(T entity) where T : class, IEntity;
        public T GetById<T>(long id) where T : class, IEntity;
        public void Remove<T>(T entity) where T : class, IEntity;
        public void Remove<T>(long id) where T : class, IEntity;
        public IList<T> GetAll<T>(Func<T, bool> filter = null) where T : class, IEntity;
    }
}
