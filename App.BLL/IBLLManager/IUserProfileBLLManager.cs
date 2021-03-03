using App.Common.Response;
using App.Models.Models;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface IUserProfileBLLManager
    {
        Task<ResponseMessage> SaveUpdateUserProfile(VMUserProfileModel model);
        Task<ResponseMessage> GetUserProfileData(long userId);
        Task<ResponseMessage> ChangePassword(VMChangePasswordModel model);
    }
}
