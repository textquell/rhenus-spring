namespace Rhenus {

    namespace Spring
    {
        namespace Configuration
        {
            internal sealed partial class Settings
            {

                public Settings()
                {
                    this.SettingChanging += this.SettingChangingEventHandler;
                    this.PropertyChanged += this.PropertyChangedEventHandler;
                    this.SettingsLoaded += this.SettingsLoadedEventHandler;
                    this.SettingsSaving += this.SettingsSavingEventHandler;
                }

                private void SettingsLoadedEventHandler( object sender, System.Configuration.SettingsLoadedEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation("Settings loaded");
                }

                private void PropertyChangedEventHandler( object sender, System.ComponentModel.PropertyChangedEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Property changed" );
                }

                private void SettingChangingEventHandler( object sender, System.Configuration.SettingChangingEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Settings changing" );
                }

                private void SettingsSavingEventHandler( object sender, System.ComponentModel.CancelEventArgs e )
                {
                    System.Diagnostics.Trace.TraceInformation( "Settings saved" );
                }
            }
        }
    }
}
