#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

#endregion

namespace DynamicSql
{
    public class SqlMapProvider
    {
        private static string _path;

        private static readonly Dictionary<string, SqlMap> _map = new Dictionary<string, SqlMap>();

        public static void Init(string path)
        {
            _path = path;
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

            var watcher = new FileSystemWatcher();
            watcher.Path = _path;
            watcher.Filter = "*.xml";
            watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.CreationTime | NotifyFilters.FileName |
                                   NotifyFilters.LastWrite;
            watcher.Created += Watcher_Created;
            watcher.Renamed += Watcher_Renamed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Changed += Watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (_map.ContainsKey(e.FullPath)) _map.Remove(e.FullPath);

            _map.Add(e.FullPath, LoadSqlMap(e.FullPath));
        }

        private static SqlMap LoadSqlMap(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SqlMap));
                using (var reader = new StringReader(File.ReadAllText(path, Encoding.UTF8)))
                {
                    var deserializedPerson = (SqlMap)serializer.Deserialize(reader);
                    return deserializedPerson;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (_map.ContainsKey(e.FullPath)) _map.Remove(e.FullPath);
        }

        private static void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (_map.ContainsKey(e.OldFullPath)) _map.Remove(e.OldFullPath);

            _map.Add(e.FullPath, LoadSqlMap(e.FullPath));
        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            _map.Add(e.FullPath, LoadSqlMap(e.FullPath));
        }
    }
}