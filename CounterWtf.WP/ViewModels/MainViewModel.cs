using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;
using CounterWtf.Common;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.WP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IWtfClient _client = new WPClient();
        private bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ProjectSummary> Projects { get; private set; }

        public MainViewModel()
        {
            this.Projects = new ObservableCollection<ProjectSummary>();
            _client.BusyIndicator.BusyStateChange += BusyStateChanged;
        }

        void BusyStateChanged(bool busy)
        {
            this.IsBusy = busy;
        }
        
        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Projects collection.
        /// </summary>
        public async Task LoadData()
        {
            if (_client.User == null)
            {
                MobileServiceUser user = await _client.Authenticate();

                if (user == null)
                {
                    return;
                }
            }

            var projects = await _client.GetProjectSummaries();

            this.Projects.Clear();

            foreach (var project in projects)
            {
                this.Projects.Add(project);
            }

            this.IsDataLoaded = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}