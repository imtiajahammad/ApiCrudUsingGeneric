using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class NotificationRepository: Repository<Notification>,INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext): base(dbContext)
        {

        }

        

        public async Task<IEnumerable<Notification>> GetOrderedNotifications(string userId)
            => await _dbContext.Notifications.Where(n => n.UserId == userId)
                .OrderByDescending(n => n.UserId)
                .ToListAsync();

        public async Task<int> CountUnreadedNotification(string userId) =>
            await _dbContext.Notifications.Where(n => n.UserId == userId)
            .CountAsync();
    }
}
