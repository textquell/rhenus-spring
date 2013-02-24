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
	/// Represents the control over a task that has been scheduled
	/// to exectue periodically.
	/// </summary>
	public interface IPeriodicTaskController
	{
		/// <summary>
		/// Ends the periodical execution.
		/// </summary>
		/// <remarks>
		/// If the task is currently being executed, that execution 
		/// is NOT aborted. Calling this method only prevents rescheduling 
		/// the task for any more executions.
		/// </remarks>
		void Cancel ();

		/// <summary>
		/// Starts task execution.
		/// </summary>
		/// <remarks>
		/// The task is never executed until this method is called. The 
		/// task is handed over for execution by the ThreadPool immediately
		/// and is executed again after the period defined at scheduling elapsed.
		/// </remarks>
		void Use ();
	}
}
