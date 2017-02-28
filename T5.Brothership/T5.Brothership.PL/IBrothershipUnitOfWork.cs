using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL
{
    public interface IBrothershipUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }

        void Commit();
    }
}
