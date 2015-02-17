using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class TargetService : CrudService<Target>, ITargetService
    {
        public TargetService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public Target GetWithCredential(short id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Target>()
                    .Get(t => t.TargetID == id, t => t.Credential)
                    .SingleOrDefault();
            }
        }

        public IEnumerable<TDto> GetAllWithCredentialAs<TDto>()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Target>()
                    .GetAllAs<TDto, Credential>(t => t.Credential);
            }
        }

        public string GenerateConfigFile(short id)
        {
            var target = this.GetByKey(id);

            var configFile = new StringBuilder();
            configFile.AppendLine("<appSettings>");

            configFile.AppendFormat("    <add key=\"ServerKey\" value=\"{0}\" />{1}",
                DomainContext.ServerKey, Environment.NewLine);
            
            configFile.AppendFormat("    <add key=\"TargetKey\" value=\"{0}\" />{1}",
                target.TargetKey, Environment.NewLine);
            
            configFile.AppendLine("    <add key=\"IPAddress\" value=\"*\" />");
            
            configFile.AppendFormat("    <add key=\"PortNumber\" value=\"{0}\" />{1}",
                target.PortNumber, Environment.NewLine);
            
            configFile.AppendLine("</appSettings>");

            return configFile.ToString();
        }
    }
}
