using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using Insula.Common;

namespace DeploymentCockpit.ApiDtos
{
    public class DeploymentJobDto
    {
        public int DeploymentJobID { get; set; }

        [Required(ErrorMessage = "The Project field is required.")]
        public short? ProjectID { get; set; }
        public string ProjectName { get; set; }

        public DateTime SubmissionTime { get; set; }
        public string SubmissionTimeDisplay
        {
            get { return this.SubmissionTime.GetDisplayValue(); }
        }

        public DateTime? StartTime { get; set; }
        public string StartTimeDisplay
        {
            get { return this.StartTime.GetDisplayValue(); }
        }

        public DateTime? EndTime { get; set; }
        public string EndTimeDisplay
        {
            get { return this.EndTime.GetDisplayValue(); }
        }

        public string Duration
        {
            get
            {
                if (!this.StartTime.HasValue)
                    return "-";

                return (this.EndTime ?? DateTime.UtcNow)
                    .Subtract(this.StartTime.Value)
                    .ToDisplayString();
            }
        }

        public string StatusKey { get; set; }

        [Required(ErrorMessage = "The Version field is required.")]
        [MaxLength(100, ErrorMessage = "Version string cannot be longer than 100 characters.")]
        public string ProductVersion { get; set; }

        [Required(ErrorMessage = "The Project Environment field is required.")]
        public short? ProjectEnvironmentID { get; set; }
        public string ProjectEnvironmentName { get; set; }

        [Required(ErrorMessage = "The Deployment Plan field is required.")]
        public short? DeploymentPlanID { get; set; }
        public string DeploymentPlanName { get; set; }

        public string Notes { get; set; }
        public string Errors { get; set; }
    }
}
