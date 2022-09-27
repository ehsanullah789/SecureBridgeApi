using Braintree;
using Microsoft.EntityFrameworkCore;
using secureBridge_Models.Models;
using secureBridge_Services.Data;
using secureBridge_Services.Enum;
using secureBridge_Services.Helpers;
using secureBridge_Services.Services.BrainTreeService;
using secureBridge_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Repositories.PaymentRepository
{
    public class PaymentRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IBraintreeService braintreeGateway;

        public PaymentRepository(IBraintreeService braintreeGateway, ApplicationDbContext db)
        {
            this.braintreeGateway = braintreeGateway;
            this.db = db;
        }

        public async Task<ResponseModel> BuySubscription(OrganizationVm ovm)
        {
            PaymentVm pvm = new PaymentVm();
            ResponseModel res = new ResponseModel();
            try
            {
                var gateway = braintreeGateway.GetGateway();
                var clientToken = gateway.ClientToken.Generate();
                if (!String.IsNullOrEmpty(ovm.Email) && !String.IsNullOrEmpty(ovm.BusinessName))
                {
                    if (ovm.PaymenMethod == (int)PaymentMethodEnum.Paypal || ovm.PaymenMethod == (int)PaymentMethodEnum.CreditCard)
                    {

                        var organizationId = await db.Organizations.Where(x => x.Email == ovm.Email).Select(x => x.Id).LastOrDefaultAsync();
                        var startDate = await db.Payments.Where(x => x.Id == organizationId).Select(x => x.StartDate).FirstOrDefaultAsync();
                        var payId = await db.Payments.Where(x => x.OrganizationId == organizationId).AnyAsync();
                        if (!payId)
                        {

                            var payment = new Payment()
                            {
                                PaymentMethod = ovm.PaymenMethod,
                                Price = ovm.Price,
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now.AddYears(1)
                            };
                            if (payment.Price == 100)
                            {

                            }
                        }
                        else
                        {
                            var currentDate = DateTime.Now;
                            var timeSpan = currentDate.Subtract(ovm.StartDate);
                            var totaldays = timeSpan.TotalDays;
                            if (totaldays > 356)
                            {

                            }
                        }

                    }
                }

                res.Status = true;
                res.Message = (ResponseEnum.Success).ToString();
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
