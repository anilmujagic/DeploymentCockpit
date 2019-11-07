using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using Insula.Common;
using Timer = System.Timers.Timer;

namespace DeploymentCockpit.Target
{
    class MainService
    {
        private readonly ITargetCommandProcessor _commandProcessor;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _commandProcessorWorkerTask;
        private Timer _timer;

        public MainService(ITargetCommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
        }

        /// <summary>
        /// Start service - background listener/processor of inbound commands and watcher timer
        /// </summary>
        public void Start()
        {
            try
            {
                Log.Info("Starting MainService commands processing loop...");

                _startCommandProcessingWorker();

                Log.Info("Command processing started, configuring watcher timer...");

                _timer = new Timer(5000);
                _timer.AutoReset = false;
                _timer.Elapsed += _watchTimerIsElapsed;
                _timer.Start();

            }
            catch (Exception ex)
            {
                Log.Error("MainService commands processing loop starting failed");
                Log.Exception(ex);
            }

        }

        /// <summary>
        /// Stop service - safely stopping internal watcher timer and cancelling background
        /// commands listening for and processing task
        /// </summary>
        public void Stop()
        {
            Log.Info("Received Stop service signal, stopping service");
            _timer.Stop();
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Timer elapsed event - check background task status and restart in case of fails
        /// </summary>
        /// <param name="sender">Timer fired the event</param>
        /// <param name="args">Event parameters</param>
        private void _watchTimerIsElapsed(object sender, ElapsedEventArgs args)
        {
            try
            {
                var task = _commandProcessorWorkerTask;
                if ((task.IsFaulted || task.Exception != null) && !task.IsCanceled)
                {
                    Log.Error("Command processing worker is fault:");
                    Log.Exception(task.Exception);

                    Log.Info("Attempt to restart the task");
                    _startCommandProcessingWorker();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error during checking command processor worker status, or worker restarting, is occured:");
                Log.Exception(ex);
            }
            finally
            {
                (sender as Timer).Start(); // Start new iteration
            }
        }

        /// <summary>
        /// Start background task for listening for and processing of inbound commands.
        /// </summary>
        private void _startCommandProcessingWorker()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _commandProcessorWorkerTask = Task.Run(
                () => { _commandProcessor.ProcessCommand(_cancellationTokenSource.Token); },
                _cancellationTokenSource.Token);
        }
    }
}
