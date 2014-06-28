using System;
using System.Threading.Tasks;
using Android.App;
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
    public class ProjectListActivity : Activity
    {
        private IWtfClient _client;
        private MobileServiceUser _user;
        private ProjectListAdapter _adapter;
 
        // Control references
        private ProgressBar _progressBar;
        private EditText _newProject;


        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Projects);

            _progressBar = FindViewById<ProgressBar>(Resource.Id.loadingProgressBar);
            _progressBar.Visibility = ViewStates.Gone;
            _newProject = FindViewById<EditText>(Resource.Id.textNewProject);

            // Create an adapter and bind it
            _adapter = new ProjectListAdapter(this);
            ListView list = FindViewById<ListView>(Resource.Id.listViewProjects);
            list.Adapter = _adapter;

            // Service locator pattern
            _client = TinyIoCContainer.Current.Resolve<IWtfClient>();
            _client.BusyIndicator.BusyStateChange += HandleBusyStateChange;
            
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
                OnProjectItemSelected();
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
                await _client.AddProject(project);
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
                string json = await opException.Response.Content.ReadAsStringAsync();
                var content = JObject.Parse(json);
                CreateAndShowDialog((string)content["message"], "Validation Error");
            }
            else
            {
                _newProject.Text = "";    
            }
        }

        // Called when the refresh menu opion is selected
        private async void OnProjectItemSelected()
        {
            await RefreshProjects();
        }


        private async Task RefreshProjects()
        {
            try
            {
                var projects = await _client.GetProjectSummaries();
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

        private void HandleBusyStateChange(bool busy)
        {
            if (_progressBar != null)
            {
                _progressBar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
            }
        }

        private async Task<MobileServiceUser> Authenticate()
        {
            try
            {
                MobileServiceUser user = await _client.Authenticate(this);
                return user;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "Authentication failed");
                return null;
            }
        }

        private void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}