using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region repositories
        private INotificationRepository notificationRepository;
        public INotificationRepository NotificationRepository 
            => notificationRepository ?? new NotificationRepository(_dbContext);

        #endregion

        public async Task<bool> Complete() =>await _dbContext.SaveChangesAsync() > 0;


        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
