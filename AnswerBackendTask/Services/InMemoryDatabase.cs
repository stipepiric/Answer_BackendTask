using AnswerBackendTask.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;

namespace AnswerBackendTask.Services
{
    public class InMemoryDatabase
    {
        private readonly Storage storage;
        public InMemoryDatabase(Storage storage) 
        { 
            this.storage = storage;
        }
        public T CreateEntity<T>() where T : class, IEntity, new()
        {
            return new T()
            {
                Id = GenerateId()
            };
        }
        public T CreateAndAddEntity<T>() where T : class, IEntity, new()
        {
            var entity = CreateEntity<T>();
            Repository<T>().Add(entity.Id, entity);
            return entity;
        }
        public T AddEntity<T>(T entity) where T : class, IEntity
        {
            Repository<T>().Add(entity.Id, entity);
            return entity;
        }
        public T GetById<T>(long id) where T : class, IEntity
        {
            if (Repository<T>().TryGetValue(id, out var result))
                return (T)result; return null;
        }
        public void Remove<T>(T entity) where T : class, IEntity
        {
            Remove<T>(entity.Id);
        }
        public void Remove<T>(long id) where T : class, IEntity
        {
            Repository<T>().Remove(id);
        }
        public IList<T> GetAll<T>(Func<T, bool> filter = null) where T : class, IEntity
        {
            var result = Repository<T>().Values.OfType<T>();
            if (filter != null) result = result.Where(filter);
            return result.ToList();
        }
        private IDictionary<long, IEntity> Repository<T>() where T : class, IEntity
        {
            if (this.storage.Entities.TryGetValue(typeof(T), out var result))
                return result;
            return this.storage.Entities[typeof(T)] = new Dictionary<long, IEntity>();
        }
        private long GenerateId()
        {
            Interlocked.Increment(ref this.storage.LastGeneratedId);
            return this.storage.LastGeneratedId;
        }

        private void Initialize()
        {
            for (int i = 1; i < 5; i++)
                AddEntity(new Item
                {
                    Id = i,
                    Name = "Item" + i
                });
        }

        private static Storage storageInstance = new Storage();

        public class Storage
        {
            public readonly DateTime InitializationTime = DateTime.UtcNow;
            public readonly IDictionary<Type, IDictionary<long, IEntity>> Entities = new Dictionary<Type, IDictionary<long, IEntity>>();
            public long LastGeneratedId = 100;
        }
        static InMemoryDatabase()
        {
            var db = AcquireDatabase();
            db.Initialize();
        }
        public static InMemoryDatabase AcquireDatabase()
        {
            return new InMemoryDatabase(storageInstance);
        }

        public static void Reset()
        {
            storageInstance = new Storage();
            new InMemoryDatabase(storageInstance).Initialize();
        }
    }
}