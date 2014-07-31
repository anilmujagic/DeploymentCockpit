using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface ICredentialService : ICrudService<Credential>
    {
        string EncryptPassword(string plainTextPassword);
        string DecryptPassword(string encryptedPassword);
    }
}
