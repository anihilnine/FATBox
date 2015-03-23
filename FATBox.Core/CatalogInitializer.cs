using System;
using Microsoft.Win32;

namespace FATBox.Initialization
{
    public static class CatalogInitializer
    {
        public static string WorkingFolder
        {
            get
            {
                return (string)Registry.GetValue(@"HKEY_CURRENT_USER\FATBox", "WorkingPath", null);
            }
            set
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\FATBox", "WorkingPath", value);
            }
        }

        public static bool IsInitialized()
        {
            if (String.IsNullOrEmpty(WorkingFolder)) 
                return false;
            if (!System.IO.File.Exists(WorkingFolder + @"\blueprints.json"))
                return false;
            if (new System.IO.FileInfo(WorkingFolder + @"\blueprints.json").Length == 0)
                return false;
            return true;
        }
    }
}