using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class TargetDto
    {
        public short TargetID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Computer Name cannot be longer than 200 characters.")]
        public string ComputerName { get; set; }

        [Required]
        [Range(UInt16.MinValue, UInt16.MaxValue)]
        public int? PortNumber { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Target Key cannot be longer than 200 characters.")]
        public string TargetKey { get; set; }

        [Required(ErrorMessage="The Credential field is required.")]
        public short? CredentialID { get; set; }

        public string CredentialName { get; set; }
    }
}
