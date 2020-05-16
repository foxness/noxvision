using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    public class Database
    {
        private static readonly string dbIndexName = "index";
        private static readonly string dbDirectoryName = "database";
        private static readonly char separator = ';';
        private static readonly string analysisExtension = ".json";

        private Dictionary<string, string> database;
        private static string indexPath;
        private static string dbPath;

        public Database()
        {
            var executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.GetDirectoryName(executablePath);

            dbPath = Path.Combine(directoryPath, dbDirectoryName);
            indexPath = Path.Combine(dbPath, dbIndexName);

            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }

            database = new Dictionary<string, string>();
            if (File.Exists(indexPath))
            {
                var lines = File.ReadAllLines(indexPath);
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (trimmed.IndexOf(separator) == -1)
                    {
                        continue;
                    }

                    var split = trimmed.Split(new[] { separator });
                    database.Add(split[0], split[1]);
                }
            }
        }

        private void Save()
        {
            var contents = "";
            foreach (var item in database)
            {
                contents += $"{item.Key};{item.Value}\n";
            }

            contents = contents.Trim();
            File.WriteAllText(indexPath, contents);
        }

        public void Store(string videoPath, string analysisPath)
        {
            if (HasAnalysis(videoPath))
            {
                return;
            }

            var name = Util.GetStringSha256Hash(videoPath);
            var destinationPath = Path.Combine(dbPath, name + analysisExtension);
            File.Move(analysisPath, destinationPath);

            database.Add(videoPath, name);
            Save();
        }

        public bool HasAnalysis(string videoPath)
        {
            return database.ContainsKey(videoPath);
        }

        public async Task<AnalysisInfo> Get(string videoPath)
        {
            var analysisName = database[videoPath];
            var analysisFilepath = Path.Combine(dbPath, analysisName + analysisExtension);
            var rawJson = File.ReadAllText(analysisFilepath);
            var info = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<AnalysisInfo>(rawJson));

            return info;
        }
    }
}
