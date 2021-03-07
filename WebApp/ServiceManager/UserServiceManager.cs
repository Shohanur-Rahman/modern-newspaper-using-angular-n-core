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


            /*DateTime fromDate = new DateTime(2018, 12, 27, 12, 0, 0);

            for (int i=0; i< 300; i++)
            {
                model.Name = "User " + i.ToString();
                model.Email = "user" + i.ToString() + "@gmail.com";
                model.CreatedDate = DateTime.Now;
                model.CreatedId = 1;
                if (i < 5)
                    model.RoleId = 1;
                else
                    model.RoleId = 3;

                model.CreatedDate = fromDate;
                model.CreatedId = 1;
                //model.RoleId = 3;

                fromDate = fromDate.AddDays(1);

                _userBLL.SaveUser(model);
            }*/

            model.CreatedDate = DateTime.Now;
            model.CreatedId = 1;
            model.RoleId = 3;
            return _userBLL.SaveUser(model);
        }
    }
}
