using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository.Anemic.Base
{
    public interface IRepository<T> : IBaseRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void UpdateSpecificField(T entity, params Expression<Func<T, object>>[] updatedProperties);
        void Delete(T entity);
    }
}
