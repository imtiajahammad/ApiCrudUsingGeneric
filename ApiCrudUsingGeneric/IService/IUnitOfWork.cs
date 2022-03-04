using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface IUnitOfWork: IDisposable
    {
        /*https://medium.com/codex/generic-repository-unit-of-work-patterns-in-net-b830b7fb5668*/
        public INotificationRepository NotificationRepository { get; }
        Task<bool> Complete();
    }
}
