using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterWtf.Common;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.WP
{
// ReSharper disable once InconsistentNaming
    public class WPClient : MobileServiceWtfClient
    {
        protected override Task<MobileServiceUser> PerformAuthentication(params object[] data)
        {
            return this.Client.LoginAsync(MobileServiceAuthenticationProvider.Twitter);
        }
    }
}
