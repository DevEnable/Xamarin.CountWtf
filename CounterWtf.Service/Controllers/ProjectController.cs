using System;
using System.Collections;
using System.Collections.Generic;
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
            try
            {
                return Query();
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw;
            }
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
        public async Task<IHttpActionResult> PostProject(Project project)
        {
            List<string> validationErrors = GetValidationErrors(project);

            if (validationErrors.Any())
            {
                // In a real code you should format the list.
                return BadRequest(validationErrors.First());
            }

            try
            {
                project.CreatedBy = ((ServiceUser)User).Id;
                Project current = await InsertAsync(project);
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            catch (Exception ex)
            {
                this.Services.Log.Error(ex);
                throw;
            }
            
        }

        // DELETE tables/Project/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteProject(string id)
        {
            return DeleteAsync(id);
        }

        private List<string> GetValidationErrors(Project project)
        {
            List<string> errors = new List<string>();

            if (String.IsNullOrWhiteSpace(project.Name))
            {
                errors.Add("A project name is required.");
            }

            if (this.Query().Any(p => p.Name.ToLower() == project.Name.ToLower()))
            {
                errors.Add("A project with this name already exists.");
            }

            return errors;
        }
    }
}