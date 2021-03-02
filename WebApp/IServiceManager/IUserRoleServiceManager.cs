using App.Common.Response;
using App.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface IUserRoleServiceManager
    {
        Task<ResponseMessage> GetAllRoles();
        Task<ResponseMessage> SaveUpdateRole(UserRole model);
        Task<ResponseMessage> GetRoleById(int id);
        Task<ResponseMessage> DeleteRole(UserRole model);
    }
}
