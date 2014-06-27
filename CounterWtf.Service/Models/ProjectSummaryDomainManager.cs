using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;

using AutoMapper;
using CounterWtf.Service.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service;

namespace CounterWtf.Service.Models
{
    public class ProjectSummaryDomainManager : MappedEntityDomainManager<ProjectSummary, Project>
    {
        public ProjectSummaryDomainManager(DbContext context, HttpRequestMessage request, ApiServices services) 
            : base(context, request, services)
        {
        }

        public override IQueryable<ProjectSummary> Query()
        {
            Services.Log.Info("Calling Query override.");

            // Incredibly lazy and inefficent, but I don't want to play with mapping projections.
            // e.g. http://lostechies.com/jimmybogard/2011/02/09/autoprojecting-linq-queries/
            var query = (from p in this.Context.Set<Project>()
                         let wtfs = p.Wtfs.Count()
                         select new
                         {
                             Project = p,
                             WtfCount = wtfs
                         }
                );

            List<ProjectSummary> summaries = new List<ProjectSummary>();

            foreach (var p in query)
            {
                ProjectSummary summary = new ProjectSummary();
                Mapper.Map(p.Project, summary);
                summary.WtfCount = p.WtfCount;
                summaries.Add(summary);
            }

            return summaries.AsQueryable();
        }

        public override SingleResult<ProjectSummary> Lookup(string id)
        {
            throw new NotSupportedException();
            /*
            var query = (from p in this.Context.Set<Project>()
                where p.Id == id
                let wtfs = p.Wtfs.Count()
                select new
                    {
                        Project = p,
                        WtfCount = wtfs
                    }
                );

            var result = query.FirstOrDefault();
            Services.Log.Info("Called Lookup");
            List<ProjectSummary> response = new List<ProjectSummary>();

            if (result != null)
            {
                ProjectSummary summary = Mapper.Map<ProjectSummary>(result.Project);
                summary.WtfCount = result.WtfCount;

                response.Add(summary);
            }

            return SingleResult.Create(response.AsQueryable());*/
        }

        public override Task<ProjectSummary> UpdateAsync(string id, Delta<ProjectSummary> patch)
        {
            throw new NotSupportedException();
        }

        public override Task<bool> DeleteAsync(string id)
        {
            throw new NotSupportedException();
        }
    }
}