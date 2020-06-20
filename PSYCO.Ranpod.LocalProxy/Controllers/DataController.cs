using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetify;
using Humanizer;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PSYCO.Common.BaseModels;
using PSYCO.Ranpod.LocalProxy.Models;
using PSYCO.Ranpod.LocalProxy.Models.ApiModels;
using PSYCO.Ranpod.LocalProxy.Models.Services;
using PSYCO.Ranpod.LocalProxy.RealTime;
using PSYCO.Ranpod.LocalProxy.RealTime.ViewModels;
using SharedModels;
using SharedModels.ViewModels;

namespace PYSCO.Rapod.LocalProxy.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        public async Task<ActionResult<string>> Push(LogViewModel data)
        {
            try
            {
                var item = populteListItem(data);
                ClientSessions.Add(item);

                ViewUpdater.Update();

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync("http://clients.ranpod.com/api/data/push", data);
                    return StatusCode((int)response.StatusCode, $"Server response :{await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
                throw;
            }

        }

        public ActionResult RemoveClient([FromBody] string serverId)
        {

            try
            {
                ClientSessions.Remove(serverId);

                ViewUpdater.Update();

                return Ok();
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
                throw;
            }

        }

        public ActionResult RemoveClients([FromBody] GridModel<ClientUpdateApiModel,string> vm)
        {

            try
            {
                foreach (var item in vm.Deleted)
                {
                    ClientSessions.Remove(item.ServerId);
                }
                

                ViewUpdater.Update();

                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
                throw;
            }

        }
        public ActionResult UpdateClient(ClientUpdateApiModel client)
        {
            try
            {

                ClientSessions.Update(client.ServerId, client.ServerName);

                ViewUpdater.Update();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
                throw;
            }

        }
        private SessionListItem populteListItem(LogViewModel data)
        {
            var listItem = new SessionListItem();
            listItem.Version = data.GetPMDLProStatusString1;
            var expireDate = new DateTime();
            DateTime.TryParse(data.RegistryExpireDate, out expireDate) ;
            listItem.LicenseExpireDate = expireDate == DateTime.MinValue ? TextResources.InvalidDate : $"{expireDate.ToUniversalTime().Humanize(culture: new CultureInfo("fa-IR"))} ({new PersianDateTime(expireDate).ToLongDateString()})";
            listItem.ClientIp = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            listItem.Number = ClientSessions.SessionsList.Count + 1;
            if (data.IsPMDLProRunning)
                listItem.IsRunning = TextResources.Running;
            else if (data.IsPMDLProRunning)
                listItem.IsRunning = TextResources.Stoped;

            var currentServerNameMapping = ClientSessions.SessionsList
                .FirstOrDefault(client=>client.ServerID==data.RegistryServerID.ToString())?.ServerName;
            listItem.ServerName = currentServerNameMapping != null ? currentServerNameMapping : TextResources.ServerNameNotAvailable;
            data.ServerName = listItem.ServerName;
            listItem.OrganizationId = data.RegistryOrgID.ToString();
            listItem.HardwareId = data.GetPMDLProStatusString2;
            listItem.ServerID = data.RegistryServerID.ToString();
            int protectionStatus = Constants.PMDLProStatusDriverNotLoaded;
            if (int.TryParse(data.GetPMDLProStatusString0.ToString(), out int parsedProtectionStatus))
                protectionStatus = parsedProtectionStatus;
            if (string.IsNullOrEmpty(data.RegistryPassword.ToString()))
                protectionStatus = Constants.PMDLProStatusNotConfigured;
            if (protectionStatus < Constants.PMDLProStatusNotActive && protectionStatus > Constants.PMDLProStatusDisabledBecauseOfLicense)
            {
                listItem.ProtectionStatus = TextResources.InvalidOrExpiredLicense;
            }
            switch (protectionStatus)
            {
                case Constants.PMDLProStatusDisabledBecauseOfLicense:
                    listItem.ProtectionStatus = TextResources.DisabledBecauseOfLicense;
                    break;
                case Constants.PMDLProStatusNotActive:
                    listItem.ProtectionStatus = TextResources.NotActive;
                    break;
                case Constants.PMDLProStatusDiskCryptOnly:
                    listItem.ProtectionStatus = TextResources.DiskCryptOnly;
                    break;
                case Constants.PMDLProStatusReadOnlyDiskCrypt:
                    listItem.ProtectionStatus = TextResources.ReadOnlyDiskCrypt;
                    break;
                case Constants.PMDLProStatusFullProtection:
                    listItem.ProtectionStatus = TextResources.FullProtection;
                    break;
                case Constants.PMDLProStatusDriverNotLoaded:
                    listItem.ProtectionStatus = TextResources.DriverNotRunning;
                    listItem.Version = TextResources.NotAvailable;
                    break;
                case Constants.PMDLProStatusNotConfigured:
                    listItem.ProtectionStatus = TextResources.NotConfigured;
                    break;
                default:
                    break;
            }

            listItem.Status = protectionStatus;
            return listItem;
        }



    }
}