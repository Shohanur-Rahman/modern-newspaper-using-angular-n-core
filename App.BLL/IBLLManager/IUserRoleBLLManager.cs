using App.Common.Response;
using App.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface IUserRoleBLLManager
    {
        Task<ResponseMessage> GetAllRoles();
        Task<ResponseMessage> SaveRole(UserRole model);
        Task<ResponseMessage> UpdateRole(UserRole model);
        Task<ResponseMessage> DeleteRole(UserRole model);
        Task<ResponseMessage> GetRoleById(int id);
    }
}
