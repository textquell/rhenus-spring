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

namespace Rhenus.Spring
{
    /// <summary>
    /// Provides scheduling means for <see cref="Rhenus.Spring.ITask">tasks</see>
    /// </summary>
    public interface ITaskScheduler
    {
        /// <summary>
        /// Schedules a task to run a soon as possible.
        /// </summary>
        /// <param name="task">the task to execute</param>
        void ScheduleTask( ITask task );

        /// <summary>
        /// Schedules a task to run as soon as possible when the startTime has passed.
        /// </summary>
        /// <param name="task">the task to execute</param>
        /// <param name="startTime">the time when the task should be run</param>
        /// <exception cref="System.ArgumentNullException">is thrown when the task parameter is 
        /// null</exception>
        /// <exception cref="System.ArgumentException">is thrown when startTime parameter is 
        /// DateTime.Now or earlier</exception>
        void ScheduleTask( ITask task, System.DateTime startTime );
    }
}
