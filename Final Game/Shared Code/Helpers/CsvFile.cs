using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using HuntTheWumpus.SharedCode.Helpers;
using System.Reflection;
using HuntTheWumpus.SharedCode.GameControl;
using System.ComponentModel;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public class CsvFile
    {
        private string[] ColumnHeaders;
        private Row[] DataRows;

        public CsvFile()
        {

        }

        public void Load(string filePath)
        {
            using (Stream Stream = FileUtils.GetFileStream(FileUtils.GetDataPath(filePath)))
            {
                string[] FileContents = Stream.ReadAllLines().ToArray();

                // TODO: Handle empty file
                string Header = FileContents[0];
                ColumnHeaders = Header.SplitCommaWithGrouping()
                    .Select(s => s.Trim())
                    .ToArray();
                
                DataRows = FileContents
                    .Skip(1)
                    .Where(s => s.Trim().Length > 0)
                    .Select(s => new Row(
                        ColumnHeaders,
                        s.SplitCommaWithGrouping()
                        .Select(x => x.Trim().Trim('"').Trim().Replace("\"\"", "\""))
                        .ToArray()))
                    .ToArray();
            }
        }

        public T[] BindAs<T>()
        {
            Type TargetType = typeof(T);

            List<T> Results = new List<T>();
            foreach(Row Row in DataRows)
            {
                T NewVal = (T)Activator.CreateInstance(TargetType);
                foreach (string Column in ColumnHeaders)
                {
                    PropertyInfo PropInfo = TargetType.GetRuntimeProperty(Column);
                    if (PropInfo == null)
                        Log.Warn("Failed to bind CSV column \"" + Column + "\" to type \"" + TargetType.FullName + "\"");
                    else
                        ReflectionUtils.SetProperty(NewVal, Column, Convert.ChangeType(Row[Column], PropInfo.PropertyType));
                }
                Results.Add(NewVal);
            }
            return Results.ToArray();
        }
        
        public class Row
        {
            public readonly string[] Data;
            private readonly string[] Headers;
            
            public Row(string[] headers, string[] data)
            {
                Data = data;
                Headers = headers;
            }

            public string this[string ColHeader]
            {
                get
                {
                    return Data[Array.FindIndex(Headers, s => s == ColHeader)];
                }
            }
        }
    }
}
