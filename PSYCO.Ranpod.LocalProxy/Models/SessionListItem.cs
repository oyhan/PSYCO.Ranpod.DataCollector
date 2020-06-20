using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.Models
{
    public class SessionListItem
    {

        public int Status { get; set; }
        public string ClientIp { get; set; }
        public string IsRunning { get; set; }
        public string Version { get; set; }
        public int Number { get; set; }
        public string LicenseExpireDate { get; set; }
        public string HardwareId { get; set; }
        public string ProtectionStatus { get; set; }
        public bool LicenseWillExpireSoon { get; set; }
        public string OrganizationId { get; set; }
        public string ServerName { get; set; }
        public string ServerID { get; set; }

        public DateTime LastReceivedData { get; set; }
    }
}
