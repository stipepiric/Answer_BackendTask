using System;
using System.Collections.Generic;
using AnswerKing.Core.Entities;

namespace AnswerKing.Infrastructure.Services
{
    public class Storage
    {
        public readonly DateTime InitializationTime = DateTime.UtcNow;
        public readonly IDictionary<Type, IDictionary<long, IEntity>> Entities = new Dictionary<Type, IDictionary<long, IEntity>>();
        public long LastGeneratedId = 100;
    }
}
