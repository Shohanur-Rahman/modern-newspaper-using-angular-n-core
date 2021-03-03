using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface IUserProfileService
    {
        Task<ResponseMessage> SaveUpdateUserProfile(VMUserProfileModel model);
        Task<ResponseMessage> GetUserProfileData(long userId);
        Task<ResponseMessage> ChangePassword(VMChangePasswordModel model);
    }
}
