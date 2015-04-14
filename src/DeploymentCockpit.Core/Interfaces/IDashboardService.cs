﻿using DeploymentCockpit.ApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Interfaces
{
    public interface IDashboardService
    {
        IEnumerable<ProjectVersionInfo> GetProjectVersionInfo();
    }
}
