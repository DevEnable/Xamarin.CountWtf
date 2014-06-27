using System.Threading.Tasks;
using Android.Content;
using CounterWtf.Common;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.Droid
{
    /// <summary>
    /// An Android implementation of the Azure Mobile Services client which exposes platform specific functionality.
    /// </summary>
    public class DroidClient : MobileServiceWtfClient
    {
       
        static DroidClient()
        {
            // Ensures that the Android specific platform assemblies are included.
            CurrentPlatform.Init();
        }

        /// <summary>
        /// Performs authentication using Twitter.
        /// </summary>
        /// <param name="data">Additional data containing the context which is relied on.</param>
        /// <returns>A task to notify when the authentication is complete.</returns>
        /// <remarks>This is in a platform specific class as LoginAsync needs a reference to the Android context.</remarks>
        protected override Task<MobileServiceUser> PerformAuthentication(params object[] data)
        {
            return this.Client.LoginAsync((Context)data[0], MobileServiceAuthenticationProvider.Twitter);
        }

    }
}