using Braintree;
using Microsoft.EntityFrameworkCore;
using secureBridge_Models.Models;
using secureBridge_Services.Data;
using secureBridge_Services.Enum;
using secureBridge_Services.Helpers;
using secureBridge_Services.Services.EncryptionServices;
using secureBridge_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IEncryptionService encryption;
        public UserRepository(ApplicationDbContext db, IEncryptionService encryption)
        {
            this.db = db;
            this.encryption = encryption;
        }

        public async Task<ResponseModel> UserSignUp(UserVm uvm)
        {
            ResponseModel res = new ResponseModel();
            User user = new User();
            try
            {
                if (uvm != null)
                {
                    var isUserExist = await db.Users.Where(x => x.Email == uvm.Email).AnyAsync();
                    if (!isUserExist)
                    {
                        var hashPassword = encryption.PasswordEncode(uvm.Password);
                        user.FirstName = uvm.FirstName;
                        user.LastName = uvm.LastName;
                        user.Phone = uvm.Phone;
                        user.Email = uvm.Email;
                        user.Password = hashPassword;
                        user.City = uvm.City;
                        user.State = uvm.State;
                        user.Zip = uvm.Zip;
                        await db.Users.AddAsync(user);
                        await db.SaveChangesAsync();

                        res.Status = true;
                        res.Message = ResponseEnum.Success.GetDescription().ToString();
                        res.Data = user;
                    }
                    else
                    {
                        res.Status = false;
                        res.Message = ResponseEnum.AlreadyRegistered.GetDescription().ToString();
                    }
                }
            }
            catch (Exception e)
            {
                res.Status = true;
                res.Message = e.Message;
            }

            return res;
        }

        public async Task<ResponseModel> UserLogin(LoginVm lvm)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                if (!String.IsNullOrEmpty(lvm.Email) && !String.IsNullOrEmpty(lvm.Password))
                {
                    var isRegistered = await db.Users.Where(x => x.Email == lvm.Email).AnyAsync();
                    if (isRegistered)
                    {
                        var email = await db.Users.Where(x => x.Email == lvm.Email).AnyAsync();
                        if (email)
                        {
                            var password = await db.Users.Where(x => x.Email == lvm.Email).Select(x => x.Password).FirstOrDefaultAsync();
                            var decodePassword = encryption.PasswordDecode(password);
                            if (decodePassword == lvm.Password)
                            {
                                var loginUser = await db.Users.Where(x => x.Email == lvm.Email)
                                                .Select(x => new
                                                {
                                                    x.FirstName,
                                                    x.LastName,
                                                    x.Email,
                                                    x.Phone,
                                                    x.City,
                                                    x.State,
                                                    x.Zip
                                                }).FirstOrDefaultAsync();
                                res.Status = true;
                                res.Message = ResponseEnum.Success.GetDescription().ToString();
                                res.Data = loginUser;
                            }
                        }
                        else
                        {
                            res.Status = false;
                            res.Message = ResponseEnum.Failure.GetDescription().ToString();
                            return res;
                        }

                    }
                    else
                    {
                        res.Status = false;
                        res.Message = ResponseEnum.Failure.GetDescription().ToString();
                    }
                }
            }
            catch (Exception e)
            {
                res.Status = true;
                res.Message = e.Message;
            }
            return res;
        }
    }
}
