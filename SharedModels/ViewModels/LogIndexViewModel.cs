using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.ViewModels

{
    public class LogIndexViewModel
    {
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
       
    }
}
