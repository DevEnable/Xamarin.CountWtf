using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CounterWtf.Common
{
    public class BusyStateIndicator : DelegatingHandler
    {
        int _busyCount;

        public event Action<bool> BusyStateChange;

        #region implemented abstract members of HttpMessageHandler

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //assumes always executes on UI thread
            if (_busyCount++ == 0 && BusyStateChange != null)
            {
                BusyStateChange(true);
            }

            var response = await base.SendAsync(request, cancellationToken);

            // assumes always executes on UI thread
            if (--_busyCount == 0 && BusyStateChange != null)
            {
                BusyStateChange(false);
            }

            return response;
        }

        #endregion
    }
}
