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
    /// Controller for the WTFs that need to be counted, for science!
    /// </summary>
    [AuthorizeLevel(AuthorizationLevel.User)] 
    public class WtfController : TableController<Wtf>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CounterWtfContext context = new CounterWtfContext();
            DomainManager = new EntityDomainManager<Wtf>(context, Request, Services);
        }

        // GET tables/Wtf
        public IQueryable<Wtf> GetAllWtfs()
        {
            return Query();
        }

        // GET tables/Wtf/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Wtf> GetWtf(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Wtf/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Wtf> PatchWtf(string id, Delta<Wtf> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Wtf/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostWtf(Wtf item)
        {
            item.CreatedBy = ((ServiceUser) User).Id;
            Wtf current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Wtf/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteWtf(string id)
        {
            return DeleteAsync(id);
        }
    }
}