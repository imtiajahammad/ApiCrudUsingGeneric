using ApiCrudUsingGeneric.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class UnitOfWorkEntityFramework: IUnitOfWorkEntityFramework
    {
        private readonly ApplicationDbContext _dbContext;
        //public IUserMasterCommand UserMasterCommand { get; }
        //public IAssignedRolesCommand AssignedRolesCommand { get; }
        //public IUserTokensCommand UserTokensCommand { get; }
        public IMoviesCommand MoviesCommand { get; }
        public UnitOfWorkEntityFramework(ApplicationDbContext context)
        {
            _dbContext = context;
            MoviesCommand = new MoviesCommand(_dbContext);
        }

        public bool Commit()
        {
            bool returnValue = true;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return returnValue;
        }

        public void Dispose()
        {
            Dispose(true);
        }


        private bool _disposedValue = false; // To detect redundant calls  

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposedValue = true;
        }
    }
}
