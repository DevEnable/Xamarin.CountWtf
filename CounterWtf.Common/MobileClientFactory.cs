using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.Common
{
    internal static class MobileClientFactory
    {
        private const string ApplicationUrl = @"https://counterwtf.azure-mobile.net/";
        private const string ApplicationKey = @"ddjldEDUWmSdHnMurGuEbAOJDtCEOA59";

        public static MobileServiceClient GetClient(HttpMessageHandler handler)
        {
            return new MobileServiceClient(ApplicationUrl, ApplicationKey, handler);
        }
    }
}
