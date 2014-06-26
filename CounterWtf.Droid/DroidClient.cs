using System.Threading.Tasks;
using Android.App;
using CounterWtf.PCL;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.Droid
{
    /// <summary>
    /// An Android implementation of the Azure Mobile Services client which exposes platform specific functionality.
    /// </summary>
    public class DroidClient : MobileServicesWtfClient
    {
       
        static DroidClient()
        {
            // Ensures that the Android specific platform assemblies are included.
            CurrentPlatform.Init();
        }

        /// <summary>
        /// Performs authentication using Twitter.
        /// </summary>
        /// <returns>A task to notify when the authentication is complete.</returns>
        /// <remarks>This is in a platform specific class as LoginAsync needs a reference to the Android context.</remarks>
        public override Task<MobileServiceUser> PerformAuthentication()
        {
            return this.Client.LoginAsync(Application.Context, MobileServiceAuthenticationProvider.Twitter);
        }

    }
}