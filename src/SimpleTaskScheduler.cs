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
using System.Collections.Generic;

namespace Rhenus.Spring
{
    /// <summary>
    /// A simple implementation of the <see cref="Rhenus.Spring.ITaskScheduler">TaskScheduler
    /// </see> Interface.
    /// </summary>
    /// <remarks>
    /// This class is working with the <see cref="System.Threading.ThreadPool">ThreadPool</see>
    /// to execute tasks. When a task is scheduled without further details, its 
    /// <see cref="Rhenus.Spring.ITask.Run">Run</see> method is handed over to the ThreadPool
    /// for execution. 
    /// </remarks>
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
            if ( task == null ) { throw new System.ArgumentNullException(); }
            if ( startTime <= DateTime.Now ) { throw new System.ArgumentNullException(); }
            DelayedTask taskToExecute = new DelayedTask( task, startTime );
        }

        private class DelayedTask
        {
            private ITask taskToRun;
            private Timer timer;

            public DelayedTask( ITask task, DateTime startTime )
            {
                this.taskToRun = task;
                 timer = new Timer(new TimerCallback( taskToRun.Run ), null, startTime-DateTime.Now, new TimeSpan(-1));
            }

            private void timeToRun( object sender, System.Timers.ElapsedEventArgs args )
            {
                System.Threading.ThreadPool.QueueUserWorkItem( new WaitCallback( taskToRun.Run ) );
            }
        }
    }
}
