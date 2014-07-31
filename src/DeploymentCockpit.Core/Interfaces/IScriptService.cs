using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IScriptService : ICrudService<Script>
    {
        Script GetWithProject(short id);
        Script GetWithParameters(short id);
        IEnumerable<TDto> GetAllWithProjectAs<TDto>();
        IEnumerable<TDto> GetAllAvailableToProjectAs<TDto>(short projectID);
    }
}
