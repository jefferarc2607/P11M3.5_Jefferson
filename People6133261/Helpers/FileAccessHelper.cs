using System;
using System.Collections.Generic;
using System.Text;

namespace People6133261.Helpers
{
    internal class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(path, filename);
        }
    }
}
