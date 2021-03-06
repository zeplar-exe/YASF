namespace YASF.Settings.Exceptions
{
    public class MissingSettingException : SettingsException
    {
        public MissingSettingException(string setting) : base($"The setting '{setting}' is missing.") { }
    }
}