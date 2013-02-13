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

namespace Rhenus
{
    namespace Spring
    {
        class Service
        {
            #region Fields
            public ITaskScheduler TaskScheduler { get; private set; }
            #endregion

            static void Main( string[] args )
            {
                if ( args.Length >= 1 )
                {
                    // TODO: for '-help' print a small help, for '-license' print some license information. See http://www.gnu.org/licenses/gpl.html -> How to apply...
                }
                else //assume that the user wants to start the service
                {
                    Service currentService = new Service();
                    currentService.TaskScheduler = new SimpleTaskScheduler();
                    Console.WriteLine(Configuration.Settings.Default.MyFirstSetting);

                    // TODO: Keep the application running and wait for someone telling it to stop
                }
            }
        }
    }
}
