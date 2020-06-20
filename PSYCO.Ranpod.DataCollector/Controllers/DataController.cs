using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PSYCO.Ranpod.DataCollector.Data;
using PSYCO.Ranpod.DataCollector.DomainObjects;
using SharedModels.ViewModels;

namespace PYSCO.Ranpod.DataCollector.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        public DataController(AppDbContext db)
        {
            _dbContext = db;
        }
        [HttpPost("push")]
        [AllowAnonymous]
        public ActionResult<string> Push(LogViewModel value)
        {
            ActionResult<string> result;
            try
            {
                var RegistryExpireDate = new DateTime();
                DateTime.TryParse(value.RegistryExpireDate, out RegistryExpireDate);
                _dbContext.Set<LogModel>().Add(new LogModel()
                {
                    Data = JsonConvert.SerializeObject(value),
                    CreatedDate = DateTime.Now,
                    GetPMDLProStatusString0 = value.GetPMDLProStatusString0,
                    GetPMDLProStatusString1 = value.GetPMDLProStatusString1,
                    GetPMDLProStatusString2 = value.GetPMDLProStatusString2,
                    IsPMDLProRunning = value.IsPMDLProRunning,
                    RegistryAppID = value.RegistryAppID,
                    RegistryDefaultProtectionMode = value.RegistryDefaultProtectionMode,
                    RegistryExpireDate = RegistryExpireDate,
                    RegistryLicenseData = value.RegistryLicenseData,
                    RegistryOrgID = value.RegistryOrgID,
                    RegistryPassword = value.RegistryPassword,
                    RegistryRestrictedPath = string.Join(',', value.RegistryRestrictedPath),
                    RegistryTrustedPath = string.Join(',', value.RegistryTrustedPath),
                    ServerName = value.ServerName

                });
                _dbContext.SaveChanges();
                result = this.Ok();
            }
            catch (Exception e)
            {
                result = this.StatusCode(500, e.ToString());
            }
            return result;
        }


        [HttpGet("GetAll")]
        public ActionResult<string> GetAll()
        {
            ActionResult<string> result;
            try
            {
                var list = _dbContext.Set<LogModel>()
                    .OrderByDescending(l => l.CreatedDate)
                    .GroupBy(l => new
                    {
                        l.RegistryOrgID,
                        l.RegistryServerID,
                        l.GetPMDLProStatusString2
                    }
                    ).Select(s => s.FirstOrDefault())
                    .ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                result = this.StatusCode(500, e.ToString());
                return result;

            }

        }

        [HttpGet("GetAllDetailed")]
        public ActionResult<string> GetAllDetailed()
        {
            ActionResult<string> result;
            try
            {
                var list = _dbContext.Set<LogModel>()
                    .OrderByDescending(l => l.CreatedDate).
                    Select(value=> new LogIndexViewModel()
                    {
                        GetPMDLProStatusString0 = value.GetPMDLProStatusString0,
                        GetPMDLProStatusString1 = value.GetPMDLProStatusString1,
                        GetPMDLProStatusString2 = value.GetPMDLProStatusString2,
                        IsPMDLProRunning = value.IsPMDLProRunning,
                        RegistryAppID = value.RegistryAppID,
                        RegistryDefaultProtectionMode = value.RegistryDefaultProtectionMode,
                        RegistryExpireDate = value.RegistryExpireDate,
                        RegistryLicenseData = value.RegistryLicenseData,
                        RegistryOrgID = value.RegistryOrgID,
                        RegistryPassword = value.RegistryPassword,
                    })
                    
                    .ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                result = this.StatusCode(500, e.ToString());
                return result;

            }

        }


    }
}