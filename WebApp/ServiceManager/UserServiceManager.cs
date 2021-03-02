using App.BLL.IBLLManager;
using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class UserServiceManager : IUserServiceManager
    {
        private readonly IUserBLLManager _userBLL;
        public UserServiceManager(IUserBLLManager userBLL)
        {
            _userBLL = userBLL;
        }

        public ResponseMessage DeleteUser(VMUserModel model)
        {
            return _userBLL.DeleteUser(model);
        }

        public ResponseMessage GetAllUsers()
        {
            return _userBLL.GetAllUsers();
        }

        public ResponseMessage Login(VMLoginModel model)
        {
            return _userBLL.Login(model);
        }

        public ResponseMessage SaveUpdateUser(VMUserModel model)
        {
            if(model.Id > 0)
            {
                model.EditedDate = DateTime.Now;
                model.EditedId = 1;
                return _userBLL.UpdateUser(model);
            }

            model.CreatedDate = DateTime.Now;
            model.CreatedId = 1;
            model.RoleId = 3;
            return _userBLL.SaveUser(model);
        }
    }
}
