using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CounterWtf.Common;
using Java.Interop;
using Microsoft.WindowsAzure.MobileServices;
using TinyIoC;

namespace CounterWtf.Droid
{
    [Activity(Label = "@string/project_wtfs")]
    public class ProjectWtfsActivity : WtfActivity
    {
        private string _projectId;
        private ProjectWtfListAdapter _adapter;

        // Control references
        private TextView _projectName;
        private TextView _wtfCount;
        private Button _addWtfButton;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ProjectWtfs);

            ProgressBar = FindViewById<ProgressBar>(Resource.Id.loadingProgressBar);
            ProgressBar.Visibility = ViewStates.Gone;
            
            _projectName = FindViewById<TextView>(Resource.Id.projectName);
            _addWtfButton = FindViewById<Button>(Resource.Id.wtfButton);
            _wtfCount = FindViewById<TextView>(Resource.Id.totalWtfs);

            _projectId = Intent.GetStringExtra("projectId");

            _projectName.Text = Intent.GetStringExtra("projectName");
            _addWtfButton.Click += (sender, e) => RegisterWtf();

            // Create an adapter and bind it
            _adapter = new ProjectWtfListAdapter(this, Resource.Layout.Row_List_Wtf);
            ListView list = FindViewById<ListView>(Resource.Id.listViewProjects);
            list.Adapter = _adapter;
            // Service locator pattern
            Client = TinyIoCContainer.Current.Resolve<IWtfClient>();
            Client.BusyIndicator.BusyStateChange += HandleBusyStateChange;

            await RefreshWtfs();
        }

        /// <summary>
        /// Initializes the activity menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.activity_main, menu);
            return true;
        }

        //Select an option from the menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_refresh)
            {
                OnWtfRefreshRequested();
            }

            return true;
        }

        // Called when the refresh menu opion is selected
        private async void OnWtfRefreshRequested()
        {
            await RefreshWtfs();
        }

        /// <summary>
        /// Registers a WTF occurance against the project.
        /// </summary>
        [Export]
        private async void RegisterWtf()
        {
            MobileServiceInvalidOperationException opException = null;

            try
            {
                // The server will assign the date and user.
                await Client.AddWtf(new Wtf
                {
                    ProjectId = _projectId
                });

                // More lazy
                await RefreshWtfs();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                opException = ex;
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
                return;
            }

            if (opException != null)
            {
                await ProcessOperationException(opException);
            }
        }

        private async Task RefreshWtfs()
        {
            try
            {
                var wtfs = (await Client.GetWtfs(_projectId)).ToList();
                
                _wtfCount.Text = wtfs.Count.ToString(CultureInfo.InvariantCulture);
                _adapter.Clear();
                
                foreach (Wtf wtf in wtfs)
                {
                    _adapter.Add(wtf);
                }
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "An error occurred while retrieving the WTFs");
            }
        }


    }
}