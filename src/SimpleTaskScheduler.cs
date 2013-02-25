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
            if ( task == null )
            {
                throw new System.ArgumentNullException( Resources.ErrorMessages.TaskNullException );
            }
            ThreadPool.QueueUserWorkItem( new WaitCallback( task.Run ) );
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Reliability", "CA2000" )]
        public void ScheduleTask( ITask task, DateTime startTime )
        {
            if ( task == null )
            {
                throw new System.ArgumentNullException( Resources.ErrorMessages.TaskNullException );
            }
            if ( startTime <= DateTime.Now )
            {
                throw new System.ArgumentException( Resources.ErrorMessages.StartTimeError );
            }
            try
            {
                DelayedTask taskToExecute = new DelayedTask( task );
                taskToExecute.Init( startTime );
            }
            catch ( ArgumentOutOfRangeException exception )
            {
                throw new ArgumentNullException( "Delay was to short", exception );
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Reliability", "CA2000" )]
        public IPeriodicTaskController SchedulePeriodicTask( ITask task, TimeSpan period )
        {
            if ( task == null )
            {
                throw new System.ArgumentNullException( Resources.ErrorMessages.TaskNullException );
            }
            if ( period.Ticks <= 0 )
            {
                throw new System.ArgumentException(Resources.ErrorMessages.TaskPeriodError);
            }
            return new PeriodicTask( task, period );
        }

        #region Helper Classes
        /// <summary>
        /// Delays a task with a <see cref="System.Threading.Timer"/>.
        /// </summary>
        /// <remarks>
        /// A delayed task takes the scheduled task and the start time are constructor arguments.
        /// When it is created, it starts a new timer and waits till it elapses before queueing the 
        /// task for execution by the ThreadPool
        /// </remarks>
        private class DelayedTask: IDisposable
        {
            private ITask taskToRun;
            private Timer timer;

            /// <summary>
            /// Convenience constructor
            /// </summary>
            /// <param name="task">the scheduled task</param>
            /// <param name="startTime">the time at which the task should run.</param>
            public DelayedTask( ITask task )
            {
                this.taskToRun = task;
            }

            public void Init( DateTime startTime )
            {
                // period of -1 indicates to call the method only once.
                timer = new Timer( new TimerCallback( taskToRun.Run ), null, startTime - DateTime.Now, new TimeSpan( -1 ) );
            }

            /// <summary>
            /// Enqueues the task to the ThreadPool.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1811" )]
            private void TimeToRun( object sender, System.Timers.ElapsedEventArgs args )
            {
                ThreadPool.QueueUserWorkItem( new WaitCallback( taskToRun.Run ) );
                this.Dispose();
            }

            public void Dispose()
            {
                this.timer.Dispose();
                this.taskToRun = null;
            }
        }

        private class PeriodicTask: IPeriodicTaskController, IDisposable
        {
            private ITask taskToRunPeriodically;
            private Timer timer;
            private TimeSpan period;

            public PeriodicTask( ITask task, TimeSpan period )
            {
                this.taskToRunPeriodically = task;
                this.period = period;
            }

            #region IPeriodicTaskController implementation
            public void Cancel()
            {
                this.Dispose();
            }
            public void Use()
            {
                this.timer = new Timer( new TimerCallback( this.Execute ), null, new TimeSpan( 0 ), this.period );
            }
            #endregion

            private void Execute( object state )
            {
                ThreadPool.QueueUserWorkItem( new WaitCallback( taskToRunPeriodically.Run ) );
            }

            public void Dispose()
            {
                this.timer.Dispose();
                taskToRunPeriodically = null;
            }
        }
        #endregion
    }
}
