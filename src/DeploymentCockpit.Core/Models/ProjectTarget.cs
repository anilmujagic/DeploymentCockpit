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
    
    public partial class ProjectTarget
    {
        public int ProjectTargetID { get; set; }
        public short TargetGroupID { get; set; }
        public short ProjectEnvironmentID { get; set; }
        public short TargetID { get; set; }
    
        public virtual ProjectEnvironment ProjectEnvironment { get; set; }
        public virtual Target Target { get; set; }
        public virtual TargetGroup TargetGroup { get; set; }
    }
}