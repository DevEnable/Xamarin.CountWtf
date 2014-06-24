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

        public override SingleResult<ProjectSummary> Lookup(string id)
        {
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
            List<ProjectSummary> response = new List<ProjectSummary>();

            if (result != null)
            {
                ProjectSummary summary = Mapper.Map<ProjectSummary>(result.Project);
                summary.WtfCount = result.WtfCount;

                response.Add(summary);
            }

            return SingleResult.Create(response.AsQueryable());
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