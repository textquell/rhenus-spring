namespace Rhenus
{
    namespace Spring
    {
        namespace Configuration
        {
            internal sealed partial class Settings: global::System.Configuration.ApplicationSettingsBase
            {

                private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized( new Settings() )));

                public static Settings Default
                {
                    get
                    {
                        return defaultInstance;
                    }
                }

                [global::System.Configuration.ApplicationScopedSettingAttribute()]
                [global::System.Configuration.DefaultSettingValueAttribute( "Some Setting" )]
                public string MyFirstSetting
                {
                    get
                    {
                        return ((string)(this["MyFirstSetting"]));
                    }
                }
            }
        }
    }
}