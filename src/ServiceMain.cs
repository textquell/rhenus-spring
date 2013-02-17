﻿/*    The Rhenus project
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

namespace Rhenus
{
    namespace Spring
    {

        // TODO: Strong-name assembly file
        // TODO: Find out if it is possible to add a 'Parallel' attribute to a class that executes all the classes methods through the task scheduler

        class Service
        {
            #region Fields
            public ITaskScheduler TaskScheduler { get; private set; }
            #endregion

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
                    currentService.TaskScheduler = new SimpleTaskScheduler();

                    // TODO: Keep the application running and wait for someone telling it to stop
                }
            }

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
