using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using CounterWtf.Service.DataObjects;
using CounterWtf.Service.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace CounterWtf.Service.Controllers
{
    /// <summary>
    /// Controller for the projects available in the counter application.
    /// </summary>
    [AuthorizeLevel(AuthorizationLevel.User)] 
    public class ProjectController : TableController<Project>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CounterWtfContext context = new CounterWtfContext();
            DomainManager = new EntityDomainManager<Project>(context, Request, Services);
        }

        // GET tables/Project
        public IQueryable<Project> GetAllProjects()
        {
            return Query();
        }

        // GET tables/Project/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Project> GetProject(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Project/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Project> PatchProject(string id, Delta<Project> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Project/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostProject(Project item)
        {
            item.UserId = ((ServiceUser)User).Id;
            Project current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Project/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteProject(string id)
        {
            return DeleteAsync(id);
        }
    }
}