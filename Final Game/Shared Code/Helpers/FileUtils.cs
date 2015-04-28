using System;
using System.IO;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public static class FileUtils
    {
        public const string AppName = "HuntTheWumpus";

        public static Stream GetFileStream(string FilePath, FileMode Mode = FileMode.OpenOrCreate)
        {
#if DESKTOP
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            return new System.IO.FileStream(FilePath, Mode);
#elif NETFX_CORE
            // TODO: Handle this in modern apps
            throw new NotImplementedException();
#endif
        }

        public static string GetConfigPath(string FileName)
        {
#if DESKTOP
            string DataDir = Environment.ExpandEnvironmentVariables("%PROGRAMDATA%\\" + AppName);
#elif NETFX_CORE
            // In modern apps, the file system is isolated to our program anyway,
            //  so we don't need to give it any specificity
            string DataDir = "";
#endif

            return Path.Combine(DataDir, FileName);
        }
    }
}
