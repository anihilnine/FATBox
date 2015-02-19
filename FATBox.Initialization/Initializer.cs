using System;
using Microsoft.Win32;

namespace FATBox.Initialization
{
    public static class Initializer
    {
        public static string WorkingFolder
        {
            get
            {
                return (string)Registry.GetValue(@"HKEY_CURRENT_USER\FATBox", "WorkingPath", null);
            }
            internal set
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\FATBox", "WorkingPath", value);
            }
        }

        static Initializer()
        {
            EnsureInitialized();
        }

        public static void EnsureInitialized()
        {
            if (!IsInitialized())
                Initialize();
        }

        private static bool IsInitialized()
        {
            if (String.IsNullOrEmpty(WorkingFolder)) 
                return false;
            if (!System.IO.File.Exists(WorkingFolder + @"\blueprints.json"))
                return false;
            if (new System.IO.FileInfo(WorkingFolder + @"\blueprints.json").Length == 0)
                return false;
            return true;
        }

        private static void Initialize()
        {
            var f = new InitializationForm();
            f.ShowDialog();
            if (!IsInitialized())
            {
                throw new Exception("FATBox initialization failed");
            }
        }
    }
}