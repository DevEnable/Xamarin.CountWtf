using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.Common
{
    public abstract class MobileServiceWtfClient : IWtfClient
    {
        private readonly BusyStateIndicator _indicator = new BusyStateIndicator();
        private readonly MobileServiceClient _client;

        public MobileServiceUser User { get; private set; }

        public BusyStateIndicator BusyIndicator
        {
            get
            {
                return _indicator;
            }
        }

        protected MobileServiceClient Client
        {
            get
            {
                return _client;
            }
        }

        protected MobileServiceWtfClient()
        {
            _client = MobileClientFactory.GetClient(_indicator);
        }

        protected abstract Task<MobileServiceUser> PerformAuthentication(params object[] data);

        public async Task<MobileServiceUser> Authenticate(params object[] data)
        {
            this.User = await PerformAuthentication(data);
            return this.User;
        }

        public Task AddProject(Project project)
        {
            return this.Client.GetTable<Project>()
                              .InsertAsync(project);
        }

        public Task<IEnumerable<ProjectSummary>> GetProjectSummaries()
        {
            return this.Client.GetTable<ProjectSummary>()
                              .ToEnumerableAsync();
        }

        public Task AddWtf(Wtf wtf)
        {
            return this.Client.GetTable<Wtf>()
                              .InsertAsync(wtf);
        }

        public Task<IEnumerable<Wtf>> GetWtfs(string projectId)
        {
            return this.Client.GetTable<Wtf>()
                              .Where(wtf => wtf.ProjectId == projectId)
                              //.OrderByDescending(wtf => wtf.CreatedAt)
                              .ToEnumerableAsync();
        }
    }
}
