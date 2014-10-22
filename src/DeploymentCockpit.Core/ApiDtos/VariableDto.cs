using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;

namespace DeploymentCockpit.ApiDtos
{
    public class VariableDto : IValidatableObject
    {
        public int VariableID { get; set; }

        [Required]
        public string ScopeKey { get; set; }
        [Required]
        public int ScopeID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public string Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VariableHelper.AllPredefined.Contains(this.Name))
            {
                yield return new ValidationResult(
                    "A variable cannot have the same name as one of the predefined variables.",
                    new[] { "Name" });
            }
        }
    }
}
