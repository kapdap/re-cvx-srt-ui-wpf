using Microsoft.Win32;
using System;

namespace SRTPluginUIRECVXWPF.Utilities
{
    public static class RegistryHelper
    {
        public static T GetValue<T>(RegistryKey baseKey, string valueKey, T _default)
        {
            try
            {
                return (T)baseKey.GetValue(valueKey, _default);
            }
            catch (Exception ex)
            {
                Plugin.ShowExceptionMessage(ex);
                return _default;
            }
        }

        public static bool GetBoolValue(RegistryKey baseKey, string valueKey, bool _default) =>
            GetValue(baseKey, valueKey, _default ? 1 : 0) != 0;

        public static double GetDoubleValue(RegistryKey baseKey, string valueKey, double _default) =>
            Convert.ToDouble(GetValue(baseKey, valueKey, _default.ToString()));
    }
}