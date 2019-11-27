using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Repository.Interface
{
    public interface IUnitOfWork
    {
        #region Core Method
        int Save();
        Task<int> SaveAsync();

        #endregion

        IRepository<Terminal> TerminalRepository { get; }

        IRepository<User> UserRepository { get; }
    }
}
