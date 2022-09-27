using Braintree;
using Microsoft.EntityFrameworkCore;
using secureBridge_Models.Models;
using secureBridge_Services.Data;
using secureBridge_Services.Enum;
using secureBridge_Services.Helpers;
using secureBridge_Services.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Repositories.OpportunityRepository
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ApplicationDbContext db;
        public OpportunityRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<ResponseModel> AddOpportunityType(string email)
        {
            ResponseModel res = new ResponseModel();
            OpportunityTypes ot = new OpportunityTypes();
            try
            {
                var isExist = await db.Organizations.Where(x => x.Email == email).AnyAsync();
                if (isExist)
                {
                    var organization = await db.Organizations.Where(x => x.Email == email).FirstOrDefaultAsync();
                    if (organization.IsActive == true)
                    {
                        var payId = await db.Payments.Where(x => x.OrganizationId == organization.Id).Select(x => x.Id).AnyAsync();
                        if (payId)
                        {
                            ot.IsHousing = true;
                            ot.OrganizationId = organization.Id;
                            await db.OpportunityTypes.AddAsync(ot);
                            await db.SaveChangesAsync();

                            res.Status = true;
                            res.Message = ResponseEnum.Success.GetDescription().ToString();
                            res.Data = ot;
                        }

                    }
                    var orgId = await db.Organizations.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefaultAsync();
                }
                else
                {
                    res.Status = false;
                    res.Message = "Oraganization Not Registered";
                }

            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }
            return res;
        }

        public async Task<ResponseModel> AddOpportunity(OpportunityVm ovm, Guid OrganozationId)
        {
            ResponseModel res = new ResponseModel();
            Opportunity opp = new Opportunity();
            try
            {
                if (true)
                {

                }
                opp.Name = ovm.Name;
                opp.Description = ovm.Description;
                opp.IsJob = true;
                opp.IsHousing = true;
                opp.IsFood = true;
                opp.IsEvent = false;
                opp.OrganizationId = OrganozationId;
                await db.Opportunities.AddAsync(opp);
                await db.SaveChangesAsync();
                res.Status = true;
                res.Message = ResponseEnum.Success.GetDescription().ToString();
                res.Data = opp;
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }

        public async Task<ResponseModel> GetAllOpportunity()
        {
            ResponseModel res = new ResponseModel();
            try
            {
                Opportunity op = new Opportunity();
                var allOpportunity = await db.Opportunities.ToListAsync();
                res.Status = true;
                res.Message = ResponseEnum.Success.GetDescription().ToString();
                res.Data = allOpportunity;
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }
        public async Task<ResponseModel> GetAllOpportunityByTypes(OpportunityTypeVm ovm)
        {
            ResponseModel res = new ResponseModel();
            try
            {

                if (true)
                {

                }
                var allOpportunity = await db.Opportunities.Where(x =>
                                            (x.IsHousing == ovm.IsHousing?true:false )&& 
                                            (x.IsJob == ovm.IsJobs ? true : false) &&
                                            (x.IsFood == ovm.IsFood ? true : false) &&
                                            (x.IsEvent == ovm.IsEvent ? true : false)
                                            ).Include(x=>x.Organization)
                                            .ToListAsync();

                res.Status = true;
                res.Message = ResponseEnum.Success.GetDescription().ToString();
                res.Data = allOpportunity;
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }
        public async Task<ResponseModel> UserAppliedOpportunity(AppliedOpportunityVm avm)
        {
            ResponseModel res = new ResponseModel();
            ApplyOpportunity ap = new ApplyOpportunity();
            try
            {

                var activeUserId = await db.Users.Where(x => x.Id == avm.UserId).Select(x => x.IsActive).FirstOrDefaultAsync();
                if (activeUserId)
                {
                    var userId = await db.ApplyOpportunities
                                                                .Where(x => x.UserId == avm.UserId)
                                                                .Include(x => x.User)
                                                                .Include(x => x.Opportunity)
                                                                .Select(x => x.UserId)
                                                                .AnyAsync();
                    if (userId)
                    {
                        res.Status = false;
                        res.Message = ResponseEnum.Applied.GetDescription().ToString();
                        return res;
                    }
                    
                }
                ap.UserId = avm.UserId;
                ap.OpportunityId = avm.OpportunityId;

                await db.ApplyOpportunities.AddAsync(ap);
                await db.SaveChangesAsync();

                res.Status = true;
                res.Message = ResponseEnum.Success.GetDescription().ToString();
                res.Data = ap;
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }
        public async Task<ResponseModel> GetOpportunityByOrganizationId(Guid OrganizationId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var isOrganization = await db.Organizations.Where(x => x.Id == OrganizationId).AnyAsync();
                var Organization = await db.Organizations.Where(x => x.Id == OrganizationId).FirstOrDefaultAsync();
                if (isOrganization)
                {
                    if (Organization.IsActive == true && Organization.IsDeleted == false)
                    {
                        var allOpportunities = await db.Opportunities.Where(x => x.OrganizationId == OrganizationId).Include(x=>x.Organization).ToListAsync();
                        res.Status = true;
                        res.Message = ResponseEnum.Success.GetDescription().ToString();
                        res.Data = allOpportunities;
                    }
                }
                res.Status = false;
                res.Message = "Organization not Found";

            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }
        public async Task<ResponseModel> GetOpportunityByUserId(Guid UserId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var user = await db.Users.Where(x => x.Id == UserId).FirstOrDefaultAsync();
                if (user.IsDeleted == false && user.IsActive == true)
                {
                    var opportunityIds = await db.ApplyOpportunities
                                                            .Where(x => x.UserId == UserId)
                                                            .Select(x => x.OpportunityId)
                                                            .ToListAsync();
                    
                    object[] opportunity;
                    var opprList = new List<Opportunity>();
                    foreach (var id in opportunityIds)
                    {
                        var opprtunity = await db.Opportunities.Where(x => x.Id == id).FirstOrDefaultAsync();
                        opprList.Add(opprtunity);
                    }
                    res.Status = true;
                    res.Message = ResponseEnum.Success.GetDescription().ToString();
                    res.Data = opprList;
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = ResponseEnum.Failure.GetDescription().ToString();
            }

            return res;
        }
        public async Task<ResponseModel> GetOpportunityByOpportunityId(Guid OpportunityId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var opportunity = await db.Opportunities.Where(x => x.Id == OpportunityId).FirstOrDefaultAsync();
                if (opportunity.IsActive == true && opportunity.IsDeleted == false)
                {
                    res.Status = true;
                    res.Message = ResponseEnum.Success.GetDescription().ToString();
                    res.Data = opportunity;
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = ResponseEnum.Failure.GetDescription().ToString();
            }

            return res;
        }
        public async Task<ResponseModel> OpportunityBySearch(string searchString)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var allOpportunity = await db.Opportunities.ToListAsync();
                if (!String.IsNullOrEmpty(searchString))
                {
                    allOpportunity = await db.Opportunities.Where(x => x.Name!.Contains(searchString)).ToListAsync();
                    if (allOpportunity.Count()>0)
                    {
                        res.Status = true;
                        res.Message = ResponseEnum.Success.GetDescription().ToString();
                        res.Data = allOpportunity;
                        return res;
                    }
                    else
                    {
                        res.Status = false;
                        res.Message = ResponseEnum.RecordNotFound.GetDescription().ToString();
                        return res;
                    }
                    
                }
                else
                {
                    res.Status = true;
                    res.Message = ResponseEnum.Success.GetDescription().ToString();
                    res.Data = allOpportunity;

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
