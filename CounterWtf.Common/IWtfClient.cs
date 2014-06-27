using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.Common
{
    public interface IWtfClient
    {
        MobileServiceUser User { get; }
        BusyStateIndicator BusyIndicator { get; }
        Task<MobileServiceUser> Authenticate(params object[] data);
        Task AddProject(Project project);
        Task<IEnumerable<ProjectSummary>> GetProjectSummaries();
        Task AddWtf(Wtf wtf);
        Task<IEnumerable<Wtf>> GetWtfs(string projectId);
    }
}
