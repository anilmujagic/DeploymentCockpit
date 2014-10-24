using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using Insula.Common;

namespace DeploymentCockpit.Target
{
    class MainService
    {
        private readonly ITargetCommandProcessor _commandProcessor;
        private readonly Timer _timer;

        public MainService(ITargetCommandProcessor commandProcessor)
        {
            if (commandProcessor == null)
                throw new ArgumentNullException("commandProcessor");
            _commandProcessor = commandProcessor;

            _timer = new Timer(500);
            _timer.AutoReset = false;
            _timer.Elapsed += _timer_Elapsed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _commandProcessor.ProcessCommand();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
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
