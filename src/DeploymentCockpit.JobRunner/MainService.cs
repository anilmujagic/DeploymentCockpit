using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using DeploymentCockpit.Interfaces;
using Insula.Common;

namespace DeploymentCockpit.JobRunner
{
    class MainService
    {
        private readonly IDeploymentJobExecutionService _deploymentJobExecutionService;
        private readonly Timer _timer;

        public MainService(IDeploymentJobExecutionService deploymentJobExecutionService)
        {
            if (deploymentJobExecutionService == null)
                throw new ArgumentNullException("deploymentJobExecutionService");
            _deploymentJobExecutionService = deploymentJobExecutionService;

            _timer = new Timer(1000);
            _timer.AutoReset = false;
            _timer.Elapsed += _timer_Elapsed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _deploymentJobExecutionService.ExecuteNextDeploymentJob();
            }
            catch (Exception ex)
            {
                foreach (var error in ex.GetExceptionTreeAsFlatList())
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                (sender as Timer).Start();  //Start new iteration
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
