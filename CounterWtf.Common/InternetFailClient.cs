using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace CounterWtf.Common
{
	/// <summary>
	/// Client class in case of internet failure.
	/// </summary>
	public class InternetFailClient : IWtfClient
	{
	    private const string DummyUser = "Kim Dotcom";
	    private const string OtherDummyUser = "Your mumma!";

	    private MobileServiceUser _user;
        private readonly BusyStateIndicator _indicator = new BusyStateIndicator();

        private static readonly List<Project> Projects = new List<Project>();
        private static readonly List<Wtf> Wtfs = new List<Wtf>();

        public MobileServiceUser User
        {
            get
            {
                return _user;
            }
        }

        public BusyStateIndicator BusyIndicator
        {
            get
            {
                return _indicator;
            }
        }

	    static InternetFailClient()
	    {
	        CreateNoInternetProject();
	        CreateOtherProjects();
	    }

	    private static void CreateNoInternetProject()
	    {
            Project noWeb = new Project
            {
                Id = "1",
                CreatedBy = DummyUser,
                Name = "WHY NOT INTERWEBZ?"
            };

            Projects.Add(noWeb);

            for (int i = 1; i <= 12; i++)
            {
                Wtf wtf = new Wtf
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = DummyUser,
                    Id = i.ToString(),
                    ProjectId = "1"
                };

                Wtfs.Add(wtf);
            }
	    }

	    private static void CreateOtherProjects()
	    {
            for (int i = 2; i <= 4; i++)
            {
                Project p = new Project
                {
                    Id = i.ToString(),
                    CreatedBy = OtherDummyUser,
                    Name = "Secret Project " + i
                };

                Projects.Add(p);

                for (int j = 1; j <= i; j++)
                {
                    Wtf wtf = new Wtf
                    {
                        CreatedAt = DateTime.Now,
                        CreatedBy = DummyUser,
                        Id = ((i * 10) + j).ToString(),
                        ProjectId = i.ToString()
                    };

                    Wtfs.Add(wtf);
                }
            }
	    }

		#region IWtfClient implementation

		public Task<MobileServiceUser> Authenticate (params object[] data)
		{
		    _user = new MobileServiceUser(DummyUser);
		    return Task.FromResult(_user);
		}

		public Task AddProject (Project project)
		{
		    project.Id = Projects.Max(p => p.Id) + 1;
		    project.CreatedBy = DummyUser;
            Projects.Add(project);

		    return Task.FromResult(0);
		}

		public Task<IEnumerable<ProjectSummary>> GetProjectSummaries ()
		{
            var query = (from p in Projects
                         let wtfs = Wtfs.Count(wtf => wtf.ProjectId == p.Id)
                         orderby p.Name
                         select new
                         {
                             Project = p,
                             WtfCount = wtfs
                         }
                );

            List<ProjectSummary> summaries = new List<ProjectSummary>();

            foreach (var p in query)
            {
                ProjectSummary summary = new ProjectSummary
                {
                    Id = p.Project.Id,
                    CreatedBy = p.Project.CreatedBy,
                    Name = p.Project.Name,
                    WtfCount = p.WtfCount
                };

                summaries.Add(summary);
            }

            return Task.FromResult((IEnumerable<ProjectSummary>) summaries);
		}

		public Task AddWtf (Wtf wtf)
		{
		    wtf.CreatedAt = DateTime.Now;
		    wtf.CreatedBy = DummyUser;
            Wtfs.Add(wtf);
		    return Task.FromResult(0);
		}

		public Task<IEnumerable<Wtf>> GetWtfs (string projectId)
		{
		    return Task.FromResult(Wtfs.Where(wtf => wtf.ProjectId == projectId));
		}

		#endregion

        
	}
}

