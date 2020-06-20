using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
    public class Constants
    {
        public const int LicenseRenewDaysLimit = 30;
        public const int PMDLProStatusRunning = 1;
        public const int PMDLProStatusNotRunning = 0;
        public const int PMDLProStatusDisabledBecauseOfLicense = 0;
        public const int PMDLProStatusNotActive = 12;
        public const int PMDLProStatusDiskCryptOnly = 13;
        public const int PMDLProStatusReadOnlyDiskCrypt = 14;
        public const int PMDLProStatusFullProtection = 15;
        public const int PMDLProStatusDisconnected = 100;

        public const string PMDLProPacketErrorFlag = "error";
        public const string ServerNameMappingFileName = "mapping.rnp";
        public const string PushServerNameInfoUrl = "http://clients.ranpod.com/api/data/push";
        public static int ProxyPort = 8000;
        public const string PortSettingKey = "Port";
        public const int MaxPortNumber = 65535;
        public const int MinPortNumber = 1;
        public const int PMDLProStatusDriverNotLoaded = -1;
        public const int PMDLProStatusNotConfigured = -2;
        //public const int WebApiPort = 8001;
    }
}
