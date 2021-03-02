using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface IUserServiceManager
    {
        ResponseMessage GetAllUsers();
        ResponseMessage SaveUpdateUser(VMUserModel model);
        ResponseMessage DeleteUser(VMUserModel model);
        ResponseMessage Login(VMLoginModel model);
    }
}
