using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Services
{
    public class CredentialService : CrudService<Credential>, ICredentialService
    {
        public CredentialService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public string EncryptPassword(string plainTextPassword)
        {
            return EncryptionHelper.Encrypt(plainTextPassword,
                DomainContext.PasswordEncryptionKey, DomainContext.PasswordEncryptionSalt);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            return EncryptionHelper.Decrypt(encryptedPassword,
                DomainContext.PasswordEncryptionKey, DomainContext.PasswordEncryptionSalt);
        }
    }
}
