using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDoyle.Repository.Interface;

namespace TMDoyle.Repository.Implementation
{


    public class UnitOfWork : IUnitOfWork
    {
        #region members 
        
        ResturantEntities _context = null;
        public ResturantEntities Context { get { return _context; } }
        #endregion

        #region Constructors
        public UnitOfWork()
        {
            _context = new ResturantEntities();
        }
        public UnitOfWork(ResturantEntities dbcontext)
        {
            _context = dbcontext;
        }
        #endregion

        #region Core Method Implementation 
        public int Save()
        {
            try
            {

                return _context.SaveChanges();

            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {

                throw ex;
            }
        }

        public Task<int> SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {

                throw ex;
            }
        }
        #endregion

        #region public Repository
        private IRepository<Terminal> _terminalRepository;
        public IRepository<Terminal> TerminalRepository
        {
            get { return _terminalRepository ?? (_terminalRepository = new RepositoryBase<Terminal>(_context)); }
        }

        private IRepository<User> _userRepository;
        public IRepository<User> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new RepositoryBase<User>(_context)); }
        }

        #endregion

    }
}
