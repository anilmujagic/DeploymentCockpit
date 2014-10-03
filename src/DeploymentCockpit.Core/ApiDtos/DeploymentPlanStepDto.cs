using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class DeploymentPlanStepDto : IValidatableObject
    {
        public int DeploymentPlanStepID { get; set; }

        [Required(ErrorMessage = "The Deployment Plan field is required.")]
        public short? DeploymentPlanID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }

        [Required(ErrorMessage = "The Execution Order field is required.")]
        [Range(1, short.MaxValue)]
        public short? ExecutionOrder { get; set; }

        [Required(ErrorMessage = "The Script field is required.")]
        public short? ScriptID { get; set; }
        public string ScriptName { get; set; }

        public bool AllTargetGroups { get; set; }
        public short? TargetGroupID { get; set; }
        public string TargetGroupName { get; set; }
        public string TargetGroupInfo
        {
            get
            {
                if (this.AllTargetGroups)
                    return "All";
                else if (this.TargetGroupID.HasValue)
                    return this.TargetGroupName;
                else
                    return "-";
            }
        }

        public bool RemoteExecution { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.RemoteExecution
                && !this.AllTargetGroups
                && !this.TargetGroupID.HasValue)
            {
                yield return new ValidationResult(
                    "For Remote Execution option to work, specify Target Group or select All Target Groups option.",
                    new[] { "RemoteExecution" });
            }
        }
    }
}
