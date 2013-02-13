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
using System.Threading;

namespace Rhenus
{
    namespace Spring
    {
        class SimpleTaskScheduler: ITaskScheduler
        {
            public void ScheduleTask( ITask task )
            {
                // TODO: write a message when throwing this error. Get this message from a ressource file
                if ( task == null ) { throw new System.ArgumentNullException(); }
                ThreadPool.QueueUserWorkItem( new WaitCallback( task.Run ) );
            }

            public void ScheduleTask( ITask task, DateTime startTime )
            {
                throw new NotImplementedException();
            }
        }
    }
}
