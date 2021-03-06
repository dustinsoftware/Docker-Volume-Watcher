﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ch.darkink.docker_volume_watcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            DockerMonitor monitor = null;
            bool cancelRequested = false;

            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole())
                .BuildServiceProvider();

            try
            {
                monitor = new DockerMonitor(serviceProvider.GetService<ILoggerFactory>().CreateLogger("Console"), 500, true, 1, "npipe://./pipe/docker_engine");
                monitor.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.CancelKeyPress += (sender, e) =>
            {
                cancelRequested = true;
                e.Cancel = true;
            };

            Console.WriteLine("Docker Volume Watcher started, press CTRL+C to exit");

            while (!cancelRequested)
            {
                Thread.Sleep(100);
            }

            if (monitor != null)
            {
                monitor.Stop();
                monitor = null;
            }
        }
    }
}
