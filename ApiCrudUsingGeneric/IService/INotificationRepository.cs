using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface INotificationRepository :IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetOrderedNotifications(string userId);
        Task<int> CountUnreadedNotification(string userId);
    }
}
