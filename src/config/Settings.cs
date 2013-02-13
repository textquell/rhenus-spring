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

using System.Configuration;
using System.ComponentModel;

namespace Rhenus
{

    namespace Spring
    {
        namespace Configuration
        {
            [SettingsGroupNameAttribute( "Rhenus.Service" )]
            internal sealed class Settings: ApplicationSettingsBase
            {

                private static Settings defaultInstance = ((Settings)(ApplicationSettingsBase.Synchronized( new Settings() )));

                public static Settings Default
                {
                    get
                    {
                        return defaultInstance;
                    }
                }

                public Settings()
                {
                    this.SettingChanging += this.SettingChangingEventHandler;
                    this.PropertyChanged += this.PropertyChangedEventHandler;
                    this.SettingsLoaded += this.SettingsLoadedEventHandler;
                    this.SettingsSaving += this.SettingsSavingEventHandler;
                }

                private void SettingsLoadedEventHandler( object sender, SettingsLoadedEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Settings loaded" );
                }

                private void PropertyChangedEventHandler( object sender, PropertyChangedEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Property changed" );
                }

                private void SettingChangingEventHandler( object sender, SettingChangingEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Settings changing" );
                }

                private void SettingsSavingEventHandler( object sender, CancelEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Settings saved" );
                }

                [ApplicationScopedSettingAttribute()]
                [DefaultSettingValueAttribute( "Some Setting" )]
                public string MyFirstSetting
                {
                    get
                    {
                        return (string)this["MyFirstSetting"];
                    }
                }
            }
        }
    }
}
