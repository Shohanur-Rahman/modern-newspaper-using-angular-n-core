using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface IUserBLLManager
    {
        ResponseMessage GetAllUsers();
        ResponseMessage SaveUser(VMUserModel model);
        ResponseMessage UpdateUser(VMUserModel model);
        ResponseMessage DeleteUser(VMUserModel model);
        ResponseMessage Login(VMLoginModel model);
    }
}
