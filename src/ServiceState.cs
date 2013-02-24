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
		// TODO: Isn't a struct enough here?
		public class ServiceState
		{
			public enum State
			{
				Running = 1,
				Starting = 2,
				ShuttingDown = 3,
				Paused = 4,
				CaughtUnhandledException = 5
			}

			public State CurrentState { get; set; } 
			// TODO: Create a property that is returning the current number of available Threads in the ThreadPool
			// TODO: Create a property to get and set the min and max size of the ThreadPool
		}
	}
}
