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

namespace Rhenus.Spring
{
    namespace Resources
    {
        internal class ErrorMessages
        {
            private static global::System.Resources.ResourceManager resourceMan;

            private static global::System.Globalization.CultureInfo resourceCulture;

            internal ErrorMessages()
            {
            }

            internal static global::System.Resources.ResourceManager ResourceManager
            {
                get
                {
                    if ( object.ReferenceEquals( resourceMan, null ) )
                    {
                        global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager( "res.ErrorMessages", typeof( ErrorMessages ).Assembly );
                        resourceMan = temp;
                    }
                    return resourceMan;
                }
            }

            internal static global::System.Globalization.CultureInfo Culture
            {
                get
                {
                    return resourceCulture;
                }
                set
                {
                    resourceCulture = value;
                }
            }

            #region Error string properties
            internal static string ExceptionCaught
            {
                get
                {
                    return ResourceManager.GetString( "Exception caught", resourceCulture );
                }
            }
            internal static string TaskNullException
            {
                get
                {
                    return resourceMan.GetString( "TaskNullException", resourceCulture );
                }
            }
            internal static string StartTimeError
            {
                get
                {
                    return resourceMan.GetString( "StartTimeError", resourceCulture );
                }
            }
            internal static string TaskPeriodError
            {
                get
                {
                    return resourceMan.GetString( "TaskPeriodError", resourceCulture );
                }
            }
            #endregion
        }
    }
}
