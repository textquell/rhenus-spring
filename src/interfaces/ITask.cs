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

namespace Rhenus
{
    namespace Spring
    {
        /// <summary>
        /// This interface is representing an atomic work item, a task. 
        /// </summary>
        /// <remarks>
        /// Tasks don't need to know
        /// much about themselves, they only need to be run. So an implementing class would care
        /// about the resources a task needs and is scheduled to the 
        /// <see cref="Rhenus.Spring.ITaskScheduler"/> afterwards.
        /// </remarks>
        public interface ITask
        {
            /// <summary>
            /// Executes the task in the <see cref="Rhenus.Spring.ITaskScheduler"/>.
            /// </summary>
            /// <remarks>
            /// This method is called by the <see cref="Rhenus.Spring.ITaskScheduler"/>. This 
            /// assumes that the task has been set up beforehand and has access to all resources
            /// needed.
            /// </remarks>
            /// <param name="state">is used to hand over information objects to the local thread
            /// pool. This is needed for compliance with the 
            /// <see cref="System.Threading.WaitCallback"/> delegate</param>
            void Run( object state );
        }
    }
}
