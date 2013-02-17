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

namespace Rhenus
{
    namespace Spring
    {
        class SimpleTaskScheduler: ITaskScheduler
        {
            #region Fields
            private List<ScheduledTask> activeTasks;
            #endregion

            public SimpleTaskScheduler()
            {
                activeTasks = new List<ScheduledTask>();
                activeTasks.Clear();
            }

            public void ScheduleTask( ref ITask task )
            {
                // TODO: write a message when throwing this error. Get this message from a ressource file
                if ( task == null ) { throw new System.ArgumentNullException(); }
                ThreadPool.QueueUserWorkItem( new WaitCallback( task.Run ) );
            }

            public void ScheduleTask( ref ITask task, DateTime startTime )
            {
                if ( task == null ) { throw new System.ArgumentNullException(); }
                ScheduledTask newTask = new ScheduledTask( ref task, startTime, this );
                activeTasks.Add( newTask );
            }

            private class ScheduledTask: IDisposable
            {
                private ITask taskToRun;
                private ITaskScheduler executor;
                private System.Timers.Timer timer;

                public ScheduledTask( ref ITask task, DateTime startTime, ITaskScheduler executor)
                {
                    this.taskToRun = task;
                    this.executor = executor;
                    timer = new System.Timers.Timer( startTime.Ticks - DateTime.Now.Ticks );
                    timer.AutoReset = false;
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timeToRun);
                }

                private void timeToRun(object sender, System.Timers.ElapsedEventArgs args)
                {
                    executor.ScheduleTask( ref taskToRun );
                    this.Dispose();
                }

                public void Dispose()
                {
                    this.timer.Dispose();
                }
            }
        }
    }
}
