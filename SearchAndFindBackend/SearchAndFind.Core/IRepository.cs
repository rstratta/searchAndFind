using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void AddObject(TEntity entity);
        void UpdateObject(TEntity entity);
        void RemoveObject(TEntity Entity);
        TEntity GetById(Guid id);
    }
}
