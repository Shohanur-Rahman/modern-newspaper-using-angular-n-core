using App.BLL.IBLLManager;
using App.Models.AppContext;
using App.Models.Models;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using App.Common.Response;
using App.Common.Static;
using App.Common.Enums;
using App.Common.Constant;
using App.Common.Encription;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.BLLManager
{
    public class UserBLLManager:IUserBLLManager
    {
        private ApplicationDbContext _context;
        public UserBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseMessage DeleteUser(VMUserModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var user = _context.User.Where(u => u.Id == model.Id).FirstOrDefault();

                if(user == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                _context.User.Remove(user);
                _context.SaveChanges();

                return result = ResponseMapping.GetResponseMessage(user.Id, (int)ResponseStatus.Success, ConstantMessaages.Deleted_Success);


            }
            catch(Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public ResponseMessage GetAllUsers()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var user = (from u in _context.User select new VMUserModel() { 
                    Name=u.Name,
                    Id=u.Id,
                    Email=u.Email,
                    IsLock=u.IsLock,
                    RoleId=u.RoleId
                }).ToList();

                result = ResponseMapping.GetResponseMessage(user, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);

            }
            catch(Exception ex)
            {
                result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }

            return result;
        }

        public ResponseMessage SaveUser(VMUserModel model)
        {
            ResponseMessage result = new ResponseMessage();

            try
            {
                var existingUser = _context.User.Where(u => u.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();

                if(existingUser == null)
                {
                    var user = MappingUser(new Users(), model);
                    _context.User.Add(user);
                    _context.SaveChanges();
                    return result = ResponseMapping.GetResponseMessage(model.Id, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(model.Email, (int)ResponseStatus.Fail, ConstantMessaages.AllReadyExist);

            }
            catch(Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public ResponseMessage UpdateUser(VMUserModel model)
        {
            ResponseMessage result = new ResponseMessage();

            try
            {
                var existingUser = _context.User.Where(u => u.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();

                if (existingUser != null)
                {
                    var user = MappingUser(existingUser, model);
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();
                    return result = ResponseMapping.GetResponseMessage(user.Id, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(model.Email, (int)ResponseStatus.Fail, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public Users MappingUser(Users exUser, VMUserModel model)
        {
            exUser.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : exUser.Name;
            exUser.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : exUser.Email;
            exUser.CreatedDate = (model.CreatedDate != null) ? model.CreatedDate : exUser.CreatedDate;
            exUser.CreatedId = (model.CreatedId != null) ? model.CreatedId : exUser.CreatedId;
            exUser.EditedDate = (model.EditedDate != null) ? model.EditedDate : exUser.EditedDate;
            exUser.RoleId = (model.RoleId > 0) ? model.RoleId : exUser.RoleId;
            exUser.Password = !string.IsNullOrEmpty(model.Password) ? SimpleCryptService.Factory().Encrypt(model.Password): exUser.Password;

            return exUser;
        }

        public ResponseMessage Login(VMLoginModel model)
        {
            ResponseMessage result = new ResponseMessage();

            try
            {
                var existingUser = _context.User.Where(u => u.Email.ToLower() == model.Email.ToLower()).ToList();

                if (existingUser != null)
                {
                    var loggedInUser = (from user in existingUser
                                    where SimpleCryptService.Factory().Decrypt(user.Password) == model.Password
                                    select new VMLoginModel()
                                    {
                                        Name = user.Name,
                                        Email = user.Email,
                                        Role=(from role in _context.UserRoles
                                              where role.Id == user.RoleId 
                                              select role.Role).FirstOrDefault(),
                                        Id = user.Id
                                    }).FirstOrDefault();

                    if(loggedInUser != null)
                    {
                        return result = ResponseMapping.GetResponseMessage(loggedInUser, (int)ResponseStatus.Success, ConstantMessaages.LoginSuccessfull);
                    }

                    return result = ResponseMapping.GetResponseMessage(model.Email, (int)ResponseStatus.Fail, ConstantMessaages.InvalidPassword);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }
    }
}
