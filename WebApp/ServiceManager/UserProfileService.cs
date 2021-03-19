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
        private readonly IUploaderHelper _uploader;
        public UserProfileService(IUserProfileBLLManager profileBLL, IUploaderHelper uploader)
        {
            _profileBLL = profileBLL;
            _uploader = uploader;
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
            if (model.UserImage != null && model.UserImage.Length > 0)
            {
                model.ProfilePicture = _uploader.UploadFile(model.UserImage, "users");
            }

            return await _profileBLL.SaveUpdateUserProfile(model);
        }
    }
}
