﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public static class FileUtils
    {
        public const string AppName = "HuntTheWumpus";

        public static Stream GetFileStream(string FilePath, FileMode Mode = FileMode.OpenOrCreate)
        {
#if DESKTOP
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            return new FileStream(FilePath, Mode);
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

        public static string GetDataPath(string FileName)
        {
#if DESKTOP
            string DataDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#elif NETFX_CORE
            // TODO: Depending on our implementation of the universal
            // file API in the helpers above, we may need to change this
            string DataDir = "";
#endif

            return Path.Combine(DataDir, FileName);
        }

        public static IEnumerable<string> ReadAllLines(this Stream stream)
        {
            using (var Stream = stream)
            using (var Reader = new StreamReader(Stream))
            {
                string Line;
                while ((Line = Reader.ReadLine()) != null)
                    yield return Line;
            }
        }
    }
}
