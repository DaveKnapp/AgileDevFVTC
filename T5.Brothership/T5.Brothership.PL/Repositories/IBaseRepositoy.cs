using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.PL.Repositories
{
    internal interface IBaseRepositoy<TEntity>: IDisposable
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(TEntity entity);
        void Insert(TEntity entity);
        void Delete(int Id);
        void Update(TEntity entity);
        void Save();
    }
}
