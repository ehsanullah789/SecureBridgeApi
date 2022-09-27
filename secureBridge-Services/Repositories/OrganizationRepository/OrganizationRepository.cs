using Braintree;
using Microsoft.EntityFrameworkCore;
using secureBridge_Models.Models;
using secureBridge_Services.Data;
using secureBridge_Services.Enum;
using secureBridge_Services.Helpers;
using secureBridge_Services.Services.BrainTreeService;
using secureBridge_Services.Services.EncryptionServices;
using secureBridge_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Repositories.OrganizationRepository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IEncryptionService encryption;
        private readonly IBraintreeService braintreeGateway;
        public OrganizationRepository(ApplicationDbContext db, IEncryptionService encryption)
        {
            this.db = db;
            this.encryption = encryption;
        }
        public async Task<ResponseModel> Register(OrganizationVm ovm)   
        {
            ResponseModel res = new ResponseModel();
            Payment pay = new Payment();
            try
            {
                var IsExist = await db.Organizations.Where(x => x.Email == ovm.Email).AnyAsync();
                if (IsExist != true)
                {
                    var passwordHash = encryption.PasswordEncode(ovm.Password);
                    var organization = new Organization()
                    {
                        BusinessName = ovm.BusinessName,
                        FirstName = ovm.FirstName,
                        LastName = ovm.LastName,
                        Email = ovm.Email,
                        Password = passwordHash,
                        Address = ovm.Address,
                        City = ovm.City,
                        State = ovm.State,
                        Zip = ovm.Zip,
                    };
                    if (organization != null)
                    {
                        //var gateway = braintreeGateway.GetGateway();
                        //var clientToken = gateway.ClientToken.Generate();
                        var clientToken = "";
                        if (!String.IsNullOrEmpty(ovm.Email) && !String.IsNullOrEmpty(ovm.BusinessName))
                        {
                            if (ovm.PaymenMethod == (int)PaymentMethodEnum.Paypal || ovm.PaymenMethod == (int)PaymentMethodEnum.CreditCard)
                            {

                                if (String.IsNullOrEmpty(clientToken))
                                {
                                    pay.PaymentMethod = ovm.PaymenMethod;
                                    pay.Price = ovm.Price;
                                    pay.StartDate = DateTime.Now;
                                    pay.EndDate = DateTime.Now.AddYears(1);
                                    if (pay.Price == 100)
                                    {
                                        await db.Organizations.AddAsync(organization);
                                        await db.SaveChangesAsync();
                                        var organizationId = await db.Organizations.Where(x => x.Email == organization.Email).Select(x => x.Id).FirstOrDefaultAsync();
                                        pay.OrganizationId = organizationId;
                                        await db.Payments.AddAsync(pay);
                                        await db.SaveChangesAsync();
                                        res.Status = true;
                                        res.Message = ResponseEnum.Success.GetDescription().ToString();
                                        res.Data = new
                                        {
                                            organization,
                                            pay
                                        };
                                        return res;
                                    }
                                    else
                                    {
                                        res.Status = false;
                                        res.Message = "Amount is not Valid for this trannsaction";
                                    }
                                }
                                else
                                {
                                    var currentDate = DateTime.Now;
                                    var timeSpan = currentDate.Subtract(ovm.StartDate);
                                    var totaldays = timeSpan.TotalDays;
                                    if (totaldays > 356)
                                    {
                                        res.Status = true;
                                        res.Message = "User Not Allowed";
                                    }
                                }

                            }
                            res.Status = false;
                            res.Message = "Payment Method not allowed";
                        }
                    }
                }
                else
                {
                    res.Status = true;
                    res.Message = "Already Register";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }
            return res;
        }
        public async Task<ResponseModel> Login(LoginVm lvm)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                if (!String.IsNullOrEmpty(lvm.Email) && !String.IsNullOrEmpty(lvm.Password))
                {
                    var isRegistered = await db.Organizations.Where(x => x.Email == lvm.Email).AnyAsync();

                    if (isRegistered)
                    {
                        var orgId = await db.Organizations.Where(x => x.Email == lvm.Email).Select(x => x.Id).LastOrDefaultAsync();
                        var payId = await db.Payments.Where(x => x.OrganizationId == orgId).AnyAsync();
                        var isActivepay = await db.Payments.Where(x => x.OrganizationId == orgId).Select(x => x.IsActive).AnyAsync();
                        if (payId==true && isActivepay == true)
                        {
                            var password = await db.Organizations.Where(x => x.Email == lvm.Email).Select(x => x.Password).FirstOrDefaultAsync();
                            if (encryption.PasswordDecode(password) != lvm.Password)
                            {
                                res.Status = true;
                                res.Message = "Invalid Login Details";
                                return res;
                            }
                            var paymentId = await db.Payments.Where(x => x.OrganizationId == orgId).Select(x => x.Id).FirstOrDefaultAsync();
                            var endDate = await db.Payments.Where(x => x.Id == paymentId).Select(x => x.EndDate).FirstOrDefaultAsync();
                            var currentDate = DateTime.Now;

                            if (endDate <= currentDate)
                            {
                                res.Status = true;
                                res.Message = ResponseEnum.Success.GetDescription().ToString();
                            }
                            else
                            {
                                res.Status = false;
                                res.Message = ResponseEnum.Failure.GetDescription().ToString();
                            } 
                        } 
                    }
                    else
                    {
                        res.Status = true;
                        res.Message = "Invalid Login Details";
                    }
                }
                else
                {
                    res.Status = true;
                    res.Message = "Credentials Required";
                }

            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }
    }
}
