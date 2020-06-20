using PSYCO.JsonDatastore;
using PSYCO.Ranpod.LocalProxy.Models.Database;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.Models
{
    public static  class ClientSessions
    {
        public static List<SessionListItem> SessionsList { get; set; }
        private static AppJsonDataStore _db; 
      public static void Initiate(IJsonDataStore<JsonDatabase> db)
        {
            _db = db as AppJsonDataStore;
            SessionsList = _db.Database.Sessions.ToList();
        }
        public static void Add(SessionListItem item)
        {


            //update the last data receive time :
            item.LastReceivedData = DateTime.Now;
            //if request is coming from unknown client (not RANPOD client)
            if (item == null || (item.IsRunning != TextResources.Stoped && item.IsRunning != TextResources.Running))
                return ;

            //request is coming from RANPOD client
            //we will check if there is any item in our list with same ip
            var oldItem = SessionsList.FirstOrDefault(session => session.ClientIp == item.ClientIp);

            if (!string.IsNullOrEmpty(item.ClientIp))
            {
                //item is already in list so we remove it
                if (oldItem != null)
                {
                    SessionsList.Remove(oldItem);
                }
            }

            //client has not been configured yet
            if (item.ProtectionStatus == TextResources.NotConfigured)
            {
                var newItem = new SessionListItem
                {
                    ClientIp = item.ClientIp,
                    IsRunning = "",
                    LicenseExpireDate = "",
                    Version = "",
                    OrganizationId = "",
                    ProtectionStatus = item.ProtectionStatus,
                    LicenseWillExpireSoon = false,
                    LastReceivedData = DateTime.Now
                };
                
                SessionsList.Add(newItem);
            }

            //client has been configured and hardware id is not present
            else if (item.HardwareId != null && item.HardwareId.ToLower().Contains(Constants.PMDLProPacketErrorFlag))
            {
                var newItem = new SessionListItem
                {
                    ClientIp = item.ClientIp,
                    IsRunning = item.IsRunning,
                    Version = TextResources.NotAvailable,
                    LicenseExpireDate = item.LicenseExpireDate,
                    ServerID = item.ServerID,
                    OrganizationId = item.OrganizationId,
                    ProtectionStatus = item.ProtectionStatus,
                    LicenseWillExpireSoon = item.LicenseWillExpireSoon,
                    LastReceivedData = DateTime.Now
                };
                SessionsList.Add(newItem);
            }
            //client is ok 
            else
            {
                SessionsList.Add(item);
            }
            _db.Database.Sessions = SessionsList;
            _db.ApplyChanges();
        }

        internal static void Update(string serverId, string serverName)
        {
            var client = SessionsList.FirstOrDefault(c => c.ServerID == serverId);
            if (client!=null)
            {
                client.ServerName = serverName;
                _db.Database.Sessions = SessionsList;

                _db.ApplyChanges();
            }

        }

        internal static void Remove(string serverId)
        {
            var sessionClient = SessionsList.FirstOrDefault(c => c.ServerID == serverId);
            if (sessionClient!=null)
            {
                SessionsList.Remove(sessionClient);

            }
            _db.Database.Sessions = SessionsList;
            _db.ApplyChanges();

        }
    }
}
