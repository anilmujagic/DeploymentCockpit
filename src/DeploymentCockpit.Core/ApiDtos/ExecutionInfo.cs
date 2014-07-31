using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;

namespace DeploymentCockpit.ApiDtos
{
    public abstract class ExecutionInfo
    {
        public string StatusKey { get; set; }

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

        public Guid ExecutionReference { get; set; }
    }

    public class ExecutionDetails : ExecutionInfo
    {
        public string ExecutedScript { get; set; }
        public string ExecutionOutput { get; set; }
    }
}
