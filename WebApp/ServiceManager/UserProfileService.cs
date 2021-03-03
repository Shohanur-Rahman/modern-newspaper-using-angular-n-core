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
    public class UserProfileService: IUserProfileService
    {
        private readonly IUserProfileBLLManager _profileBLL;
        public UserProfileService(IUserProfileBLLManager profileBLL)
        {
            _profileBLL = profileBLL;
        }

        public async Task<ResponseMessage> ChangePassword(VMChangePasswordModel model)
        {
            return await _profileBLL.ChangePassword(model);
        }

        public async Task<ResponseMessage> GetUserProfileData(long userId)
        {
            return await _profileBLL.GetUserProfileData(userId);
        }

        public async Task<ResponseMessage> SaveUpdateUserProfile(VMUserProfileModel model)
        {
            return await _profileBLL.SaveUpdateUserProfile(model);
        }
    }
}
