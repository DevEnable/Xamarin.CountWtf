using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using CounterWtf.Common;
using Java.Interop;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using TinyIoC;

namespace CounterWtf.Droid
{
    /// <summary>
    /// Lists out the projects that have projects associated with them.
    /// </summary>
    [Activity(MainLauncher = true, Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ProjectListActivity : WtfActivity
    {
        private MobileServiceUser _user;
        private ProjectListAdapter _adapter;
 
        // Control references
        private EditText _newProject;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Projects);

            ProgressBar = FindViewById<ProgressBar>(Resource.Id.loadingProgressBar);
            ProgressBar.Visibility = ViewStates.Gone;
            _newProject = FindViewById<EditText>(Resource.Id.textNewProject);

            // Create an adapter and bind it
            _adapter = new ProjectListAdapter(this);
            ListView list = FindViewById<ListView>(Resource.Id.listViewProjects);
            list.Adapter = _adapter;
            list.ItemClick += ProjectTouched;
            // Service locator pattern
            Client = TinyIoCContainer.Current.Resolve<IWtfClient>();
            Client.BusyIndicator.BusyStateChange += HandleBusyStateChange;
            
            // Authenticate the user as all of the Api controllers require a validated user.
            _user = await Authenticate();

            if (_user != null)
            {
                TextView loggedInState = FindViewById<TextView>(Resource.Id.loggedInState);
                // We do not get the display name.  Azure Mobile Services can get this name for us, but that is out of scope for this demo.
                loggedInState.Text = String.Concat(this.Resources.GetString(Resource.String.logged_in_as), _user.UserId);
                await RefreshProjects();    
            }
        }

        private void ProjectTouched(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(ProjectWtfsActivity));
            ProjectSummary project = _adapter[e.Position];
            intent.PutExtra("projectId", project.Id);
            intent.PutExtra("projectName", project.Name);
            StartActivity(intent);
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
                OnProjectRefreshRequested();
            }
            return true;
        }

        [Export]
        public async void AddItem(View view)
        {
            if (string.IsNullOrEmpty(_newProject.Text))
            {
                return;
            }

            // Create a new project
            Project project = new Project
            {
                Name= _newProject.Text,
            };

            MobileServiceInvalidOperationException opException = null;

            try
            {
                // Insert the new project
                await Client.AddProject(project);
                // Lazy
                await RefreshProjects();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                // Validation exceptions will be trapped here.
                // Need to assign it a variable outside of the try/catch scope as you cannot await in here.
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
            else
            {
                _newProject.Text = "";    
            }
        }

        // Called when the refresh menu opion is selected
        private async void OnProjectRefreshRequested()
        {
            await RefreshProjects();
        }


        private async Task RefreshProjects()
        {
            try
            {
                var projects = await Client.GetProjectSummaries();
                _adapter.Clear();
                foreach (ProjectSummary project in projects)
                {
                    _adapter.Add(project);
                }
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "An error occurred while retrieving the projects");
            }
        }

        private async Task<MobileServiceUser> Authenticate()
        {
            try
            {
                MobileServiceUser user = await Client.Authenticate(this);
                return user;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "Authentication failed");
                return null;
            }
        }

    }
}