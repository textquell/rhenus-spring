/*    The Rhenus project
 *    
 *    This file is part of the Rhenus framework that aimes to be a 
 *    horizontally scalable application server with a focus on games.
 *    
 *    Copyright (C) 2013  Hans Meyer
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU General Public License as published by
 *    the Free Software Foundation, either version 3 of the License, or
 *    (at your option) any later version.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *    GNU General Public License for more details.
 *
 *    You should have received a copy of the GNU General Public License
 *    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
 */
using System;
using System.Configuration;
using System.ServiceProcess;

namespace Rhenus
{
	namespace Spring
	{

		// TODO: Strong-name assembly file
		// TODO: Find out if it is possible to add a 'Parallel' attribute to a class that executes all the classes methods through the task scheduler
		// TODO: Create a ShutDown callback for modules that is executed when the service is shutting down, so the modules can clean up and terminate gracefully.
		// TODO: Implement a mechanism that is ignoring further calls to a shutdown request of the service and is shutting it down only once.
		public class Service: ServiceBase
		{
            #region Fields
			public ITaskScheduler TaskScheduler { get; private set; }
			public ServiceState State { get; set; }
            #endregion

			public Service ()
			{
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler (this.CatchUnhandledException);
				this.TaskScheduler = new SimpleTaskScheduler ();
				this.State = new ServiceState ();

				SetServiceData ();
			}

			#region Service Control
			protected override void OnStart (string[] args)
			{
				this.State.CurrentState = ServiceState.State.Starting;



				LoadModules ();

			}

			protected override void OnContinue ()
			{
				// TODO: Continue scheduling tasks with the ITaskScheduler
				this.State.CurrentState = ServiceState.State.Running;
				base.OnContinue ();
			}

			protected override void OnPause ()
			{
				// TODO: pause scheduling tasks with the ITaskScheduler
				this.State.CurrentState = ServiceState.State.Paused;
				base.OnPause ();
			}

			protected override void OnShutdown ()
			{
				// TODO: implement a timer that is forcefully shutting down this service if the modules are not unloaded timely
				this.State.CurrentState = ServiceState.State.ShuttingDown;
				base.OnShutdown ();
			}

			protected override void OnStop ()
			{
				this.State.CurrentState = ServiceState.State.ShuttingDown;
				base.OnStop ();
			}
			#endregion

			static void Main (string[] args)
			{
				if (args.Length >= 1) {
					if (args [0] == "-license") {
						PrintLicenseInfo ();
					} else {
						PrintHelp ();
					}
				} else {
					ServiceBase.Run (new Service ());
				}
			}

			#region Helper Functions
			static void LoadModules ()
			{
				System.Configuration.Configuration conf = ConfigurationManager.OpenExeConfiguration (System.IO.Path.Combine (Environment.CurrentDirectory, "Rhenus Service.exe"));
				ConfigurationSectionGroup sections = conf.GetSectionGroup ("applicationSettings");
				ConfigurationSectionCollection definedSections = sections.Sections;
				Console.WriteLine ();
				Console.WriteLine ("The service is going to load the following modules:");
				Console.WriteLine ();
				// TODO: Using compiler switches for the versions of .NET where "var" is a valid Type?
				foreach (var currentSection in definedSections.Keys) {
					Console.WriteLine (currentSection);
				}
			}

			private void CatchUnhandledException (object sender, UnhandledExceptionEventArgs e)
			{
				State.CurrentState = ServiceState.State.CaughtUnhandledException;

				Exception criticalException = (Exception)e.ExceptionObject;
				Console.WriteLine ("Unhandled Exception caught: " + criticalException.Message);
				Console.WriteLine (criticalException.StackTrace);
			}

			private void SetServiceData ()
			{
				this.ServiceName = "Rhenus Service";
				this.CanStop = true;
				this.CanPauseAndContinue = false;
				this.CanShutdown = true;
				this.AutoLog = true;
				this.CanHandlePowerEvent = false;
				this.CanHandleSessionChangeEvent = false;
			}

			[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Globalization", "CA1303", MessageId = "System.Console.WriteLine(System.String)" )]
			private static void PrintHelp ()
			{
				Console.WriteLine ("");
				Console.WriteLine ("  The Rhenus Server Framework");
				Console.WriteLine ("");
				Console.WriteLine ("  This executable is managing the services running");
				Console.WriteLine ("  on top of it. It is scheduling tasks for them and ");
				Console.WriteLine ("  monitors their execution.");
				Console.WriteLine ("");
				Console.WriteLine ("  This project is Open Source under the terms of ");
				Console.WriteLine ("  GNU GPLv3. Find out more about the license with ");
				Console.WriteLine ("  '-license'.");
				Console.WriteLine ("");
				Console.WriteLine ("  To start the server, arguments must be empty.");
				Console.WriteLine ("");
			}

			[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Globalization", "CA1303", MessageId = "System.Console.WriteLine(System.String)" )]
			private static void PrintLicenseInfo ()
			{
				Console.WriteLine ("");
				Console.WriteLine ("  Rhenus Server  Copyright (C) 2013  Hans Meyer");
				Console.WriteLine ("");
				Console.WriteLine ("  This program comes with ABSOLUTELY NO WARRANTY.");
				Console.WriteLine ("  This is free software, and you are welcome to redistribute it");
				Console.WriteLine ("  under certain conditions; see LICENSE.md for details.");
			}
			#endregion
		}
	}
}
