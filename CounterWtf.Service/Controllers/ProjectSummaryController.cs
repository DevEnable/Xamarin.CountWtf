using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using CounterWtf.Service.DataObjects;
using CounterWtf.Service.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace CounterWtf.Service.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)] 
    public class ProjectSummaryController : TableController<ProjectSummary>
    {
        private CounterWtfContext _context;
        
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new CounterWtfContext();
            this.DomainManager = new ProjectSummaryDomainManager(_context, Request, Services);
        }

        /// <summary>
        /// Gets a full set of all of the project summaries.
        /// </summary>
        /// <returns>All of the project summaries.</returns>
        public IQueryable<ProjectSummary> GetProjectSummaries()
        {
            return this.Query();
        }
    }
}