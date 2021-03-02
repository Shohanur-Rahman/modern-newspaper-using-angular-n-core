using App.BLL.IBLLManager;
using App.Common.Constant;
using App.Common.Enums;
using App.Common.Response;
using App.Common.Static;
using App.Models.AppContext;
using App.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.BLLManager
{
    public class UserRoleBLLManager: IUserRoleBLLManager
    {
        private ApplicationDbContext _context;
        public UserRoleBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMessage> DeleteRole(UserRole model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var role = await _context.UserRoles.Where(u => u.Id == model.Id).FirstOrDefaultAsync();

                if (role == null)
                {
                    return  result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                _context.UserRoles.Remove(role);
                await _context.SaveChangesAsync();

                return result = ResponseMapping.GetResponseMessage(role.Role, (int)ResponseStatus.Success, ConstantMessaages.Deleted_Success);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetAllRoles()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var roles = await _context.UserRoles.ToListAsync();

                if (roles == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(roles, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetRoleById(int id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingRole = await _context.UserRoles.Where(r => r.Id == id).FirstOrDefaultAsync();

                if (existingRole != null)
                {
                    return result = ResponseMapping.GetResponseMessage(existingRole, (int)ResponseStatus.Fail, ConstantMessaages.RetrieveSuccess);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Success, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> SaveRole(UserRole model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingRole = await _context.UserRoles.Where(r => r.Role.ToLower() == model.Role.ToLower()).FirstOrDefaultAsync();

                if (existingRole == null)
                {
                    UserRole role = new UserRole();
                    role.Role = model.Role;
                    _context.UserRoles.Add(role);
                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(role, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.AllReadyExist);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> UpdateRole(UserRole model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingRole = await _context.UserRoles.Where(r => r.Id == model.Id).FirstOrDefaultAsync();

                if (existingRole != null)
                {
                    existingRole.Role = model.Role;
                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(existingRole, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Success, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }
    }
}
