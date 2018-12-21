using CrossoverSpa.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrossoverSpa.Helper
{
    public interface IReadOnlyRepository<T> where T : EntityBase
    {
        T FindById(int id);
        ICollection<T> Find(Expression<Func<T, bool>> predicate);
        ICollection<T> FindAll();

    }
}
