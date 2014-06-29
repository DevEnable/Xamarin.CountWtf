using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CounterWtf.Common;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace CounterWtf.Droid
{
    /// <summary>
    /// Base WTF activity containing common members.
    /// </summary>
    public class WtfActivity : Activity
    {
        protected IWtfClient Client;
        protected ProgressBar ProgressBar;

        protected void HandleBusyStateChange(bool busy)
        {
            if (ProgressBar != null)
            {
                ProgressBar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
            }
        }

        protected async Task ProcessOperationException(MobileServiceInvalidOperationException opException)
        {
            string json = await opException.Response.Content.ReadAsStringAsync();
            var content = JObject.Parse(json);
            CreateAndShowDialog((string)content["message"], "Validation Error");
        }

        protected void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        protected void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

    }
}