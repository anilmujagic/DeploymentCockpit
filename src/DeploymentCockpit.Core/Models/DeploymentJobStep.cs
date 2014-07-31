//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeploymentCockpit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeploymentJobStep
    {
        public DeploymentJobStep()
        {
            this.DeploymentJobStepTargets = new HashSet<DeploymentJobStepTarget>();
        }
    
        public int DeploymentJobStepID { get; set; }
        public int DeploymentJobID { get; set; }
        public int DeploymentPlanStepID { get; set; }
        public string StatusKey { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string ExecutedScript { get; set; }
        public string ExecutionOutput { get; set; }
        public System.Guid ExecutionReference { get; set; }
    
        public virtual DeploymentJob DeploymentJob { get; set; }
        public virtual DeploymentPlanStep DeploymentPlanStep { get; set; }
        public virtual ICollection<DeploymentJobStepTarget> DeploymentJobStepTargets { get; set; }
    }
}