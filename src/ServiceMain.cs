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

namespace Rhenus
{
    namespace Spring
    {

        // TODO: Strong-name assembly file
        // TODO: Find out if it is possible to add a 'Parallel' attribute to a class that executes all the classes methods through the task scheduler
        // TODO: Create a ShutDown callback for modules that is executed when the service is shutting down, so the modules can clean up and terminate gracefully.
        // TODO: Implement a mechanism that is ignoring further calls to a shutdown request of the service and is shutting it down only once.

        class Service
        {
            #region Fields
            ITaskScheduler TaskScheduler { get; set; }
            bool isRunning;
            #endregion

            Service()
            {
                this.TaskScheduler = new SimpleTaskScheduler();
                this.isRunning = false;
            }

            static void Main( string[] args )
            {
                #region ArgumentHandling
                if ( args.Length >= 1 )
                {
                    if ( args[0] == "-license" )
                    {
                        printLicenseInfo();
                    }
                    else
                    {
                        printHelp();
                    }
                }
                #endregion
                else //assume that the user wants to start the service
                {
                    Service currentService = new Service();
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler( currentService.CatchUnhandledException );

					System.Configuration.Configuration conf = ConfigurationManager.OpenExeConfiguration( System.IO.Path.Combine( Environment.CurrentDirectory, "Rhenus Service.exe" ) );
                    ConfigurationSectionGroup sections = conf.GetSectionGroup( "applicationSettings" );
                    ConfigurationSectionCollection definedSections = sections.Sections;
                    Console.WriteLine();
                    Console.WriteLine( "The service is going to load the following modules:" );
                    Console.WriteLine();
                    foreach ( var currentSection in definedSections.Keys )
                    {
                        Console.WriteLine( currentSection );
                    }
                    currentService.StartRunning();
                }
            }

            void StartRunning()
            {
                // TODO: Set up the Service here, loading initial modules and setting requiered properties.
                int loopPasses = 0;
                while ( this.isRunning )
                {
                    // TODO: Call a method of this class to tell it to stop. Find a way to input this shutDown command from the user side or programmatically
                    // TODO: Replace the loop count with meanigful instructions while the service is running
                    if ( loopPasses == 100 ) { this.isRunning = false; }
                    loopPasses++;
                }
                throw new DivideByZeroException( "Hihi" );
            }

            private void CatchUnhandledException( object sender, UnhandledExceptionEventArgs e )
            {
                Exception criticalException = (Exception)e.ExceptionObject;
                Console.WriteLine( "Unhandled Exception caught: " + criticalException.Message );
                Console.WriteLine( criticalException.StackTrace );
                Environment.Exit( 1 );
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Globalization", "CA1303", MessageId = "System.Console.WriteLine(System.String)" )]
            private static void printHelp()
            {
                Console.WriteLine( "" );
                Console.WriteLine( "  The Rhenus Server Framework" );
                Console.WriteLine( "" );
                Console.WriteLine( "  This executable is managing the services running" );
                Console.WriteLine( "  on top of it. It is scheduling tasks for them and " );
                Console.WriteLine( "  monitors their execution." );
                Console.WriteLine( "" );
                Console.WriteLine( "  This project is Open Source under the terms of " );
                Console.WriteLine( "  GNU GPLv3. Find out more about the license with " );
                Console.WriteLine( "  '-license'." );
                Console.WriteLine( "" );
                Console.WriteLine( "  To start the server, arguments must be empty." );
                Console.WriteLine( "" );
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Globalization", "CA1303", MessageId = "System.Console.WriteLine(System.String)" )]
            private static void printLicenseInfo()
            {
                Console.WriteLine( "" );
                Console.WriteLine( "  Rhenus Server  Copyright (C) 2013  Hans Meyer" );
                Console.WriteLine( "" );
                Console.WriteLine( "  This program comes with ABSOLUTELY NO WARRANTY." );
                Console.WriteLine( "  This is free software, and you are welcome to redistribute it" );
                Console.WriteLine( "  under certain conditions; see LICENSE.md for details." );
            }
        }
    }
}
