using System;

namespace FATBox.Core
{
    public static class FileUtil
    {
        
        public static string ExpandEnvironmentVariables(string path)
        {                
            var myDocuments = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            path = path.Replace("%Documents%", myDocuments);
            path = System.Environment.ExpandEnvironmentVariables(path);
            return path;
        }

    }
}