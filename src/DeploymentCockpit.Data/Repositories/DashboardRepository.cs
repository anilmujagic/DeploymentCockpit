using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insula.Common;

namespace DeploymentCockpit.Data.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        protected readonly DeploymentCockpitEntities _db;

        public DashboardRepository(DeploymentCockpitEntities db)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            _db = db;
        }

        public IEnumerable<ProjectVersionInfo> GetProjectVersionInfo()
        {
            var statusKey = DeploymentStatus.Finished.GetName();

            return _db.ProjectEnvironments
                .Select(e =>
                    new 
                    {
                        ProjectName = e.Project.Name,
                        EnvironmentName = e.Name,
                        DeploymentJob = e.DeploymentJobs
                            .Where(j => j.StatusKey == statusKey)
                            .OrderByDescending(j => j.DeploymentJobID)
                            .FirstOrDefault()
                    })
                .ToList()
                .Select(i =>
                    new ProjectVersionInfo
                    {
                        ProjectName = i.ProjectName,
                        EnvironmentName = i.EnvironmentName,
                        DeploymentJobID = i.DeploymentJob != null
                            ? i.DeploymentJob.DeploymentJobID as int?
                            : null,
                        DeploymentJobTime = i.DeploymentJob != null
                            ? i.DeploymentJob.SubmissionTime.ToString(DomainContext.DateTimeFormatString)
                            : null,
                        ProductVersion = i.DeploymentJob != null
                            ? i.DeploymentJob.ProductVersion
                            : null,
                    })
                .ToList();
        }
    }
}
