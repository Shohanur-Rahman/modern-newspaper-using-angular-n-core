using App.BLL.IBLLManager;
using App.Common.Response;
using App.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class UserRoleServiceManager : IUserRoleServiceManager
    {
        private readonly IUserRoleBLLManager _roleBLL;
        public UserRoleServiceManager(IUserRoleBLLManager roleBLL)
        {
            _roleBLL = roleBLL;
        }

        public async Task<ResponseMessage> DeleteRole(UserRole model)
        {
            return await _roleBLL.DeleteRole(model);
        }

        public async Task<ResponseMessage> GetAllRoles()
        {
            return await _roleBLL.GetAllRoles();
        }

        public async Task<ResponseMessage> GetRoleById(int id)
        {
            return await _roleBLL.GetRoleById(id);
        }

        public async Task<ResponseMessage> SaveUpdateRole(UserRole model)
        {
            if (model.Id > 0)
                return await _roleBLL.UpdateRole(model);
            else
                return await _roleBLL.SaveRole(model);
        }
    }
}
