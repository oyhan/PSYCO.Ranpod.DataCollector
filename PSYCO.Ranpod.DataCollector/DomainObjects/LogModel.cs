using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.DataCollector.DomainObjects
{
    public class LogModel : BaseModel<int>
    {
        public string Data { get; set; }
        public bool IsPMDLProRunning { get; set; }
        public string GetPMDLProStatusString0 { get; set; }
        public string GetPMDLProStatusString1 { get; set; }
        public string GetPMDLProStatusString2 { get; set; }
        public int RegistryAppID { get; set; }

        public int RegistryServerID { get; set; }
        public int RegistryOrgID { get; set; }
        public int RegistryDefaultProtectionMode { get; set; }
        public string RegistryLicenseData { get; set; }
        public DateTime RegistryExpireDate { get; set; }
        public string RegistryPassword { get; set; }
        public string RegistryRestrictedPath { get; set; }
        public string RegistryTrustedPath { get; set; }
        public string ServerName { get; internal set; }
    }
}
