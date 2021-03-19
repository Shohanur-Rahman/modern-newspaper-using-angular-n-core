using App.BLL.IBLLManager;
using App.Common.Constant;
using App.Common.Encription;
using App.Common.Enums;
using App.Common.Response;
using App.Common.Static;
using App.Models.AppContext;
using App.Models.Models;
using App.Models.VMModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.BLLManager
{
    public class UserProfileBLLManager : IUserProfileBLLManager
    {
        private readonly ApplicationDbContext _context;
        public UserProfileBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMessage> ChangePassword(VMChangePasswordModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var user = await _context.User.Where(u => u.Id == model.Id).FirstOrDefaultAsync();
                if(user != null)
                {
                    user.Password = SimpleCryptService.Factory().Encrypt(model.Password);
                    await _context.SaveChangesAsync();
                    return result = ResponseMapping.GetResponseMessage(model.Email, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetUserProfileData(long userId)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var userProfile = await (from profile in _context.UserProfile
                                         where profile.UserId == userId
                                         select new VMUserProfileModel()
                                         {
                                             Id = profile.Id,
                                             UserId = profile.UserId,
                                             FirstName = profile.FirstName,
                                             LastName = profile.LastName,
                                             SecondaryEmail = profile.SecondaryEmail,
                                             PrimaryMobile = profile.PrimaryMobile,
                                             SecondaryMobile = profile.SecondaryMobile,
                                             City = profile.City,
                                             House = profile.House,
                                             Street = profile.Street,
                                             Road = profile.Road,
                                             Country = profile.Country,
                                             AddressDetails = profile.AddressDetails,
                                             NID = profile.NID,
                                             UserBio = profile.UserBio,
                                             DisplayName = profile.DisplayName,
                                             ProfileDescription = profile.ProfileDescription,
                                             ProfilePicture=profile.ProfilePicture

                                         }).FirstOrDefaultAsync();

                if (userProfile == null)
                {
                    return result = ResponseMapping.GetResponseMessage( new VMUserProfileModel(), (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }
                else
                {
                    return result = ResponseMapping.GetResponseMessage(userProfile, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);
                }
            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> SaveUpdateUserProfile(VMUserProfileModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var userProfile = await _context.UserProfile.Where(u => u.UserId == model.UserId).FirstOrDefaultAsync();

                if (userProfile == null)
                {
                    userProfile = MapUserProfile(new UserProfile(), model);
                    _context.UserProfile.Add(userProfile);
                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(userProfile, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }
                else
                {
                    userProfile = MapUserProfile(userProfile, model);
                    _context.Entry(userProfile).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return result = ResponseMapping.GetResponseMessage(userProfile, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }
            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }



        private UserProfile MapUserProfile(UserProfile profile, VMUserProfileModel model)
        {
            profile.UserId = model.UserId;
            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.SecondaryEmail = model.SecondaryEmail;
            profile.PrimaryMobile = model.PrimaryMobile;
            profile.SecondaryMobile = model.SecondaryMobile;
            profile.City = model.City;
            profile.Street = model.Street;
            profile.House = model.House;
            profile.Road = model.Road;
            profile.Country = model.Country;
            profile.NID = model.NID;
            profile.UserBio = model.UserBio;
            profile.DisplayName = model.DisplayName;
            profile.ProfileDescription = model.ProfileDescription;
            profile.AddressDetails = model.AddressDetails;
            profile.ProfilePicture = (!string.IsNullOrEmpty(model.ProfilePicture)) ? model.ProfilePicture : profile.ProfilePicture;
            profile.NIDPhoto = (string.IsNullOrEmpty(model.NIDPhoto) == false) ? model.NIDPhoto : profile.NIDPhoto;
            profile.CreatedDate = (model.CreatedDate == null) ? profile.CreatedDate : model.CreatedDate;
            profile.CreatedId = (model.CreatedId == null) ? profile.CreatedId : model.CreatedId;
            profile.EditedDate = (model.EditedDate == null) ? profile.EditedDate : model.EditedDate;
            profile.EditedId = (model.EditedId == null) ? profile.EditedId : model.EditedId;

            return profile;
        }
    }
}
