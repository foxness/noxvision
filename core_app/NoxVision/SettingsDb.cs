using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    public class SettingsDb
    {
        private static readonly string dbFilename = "settings";
        private static readonly char separator = ';';

        private static string dbPath;

        public int ConfidenceThreshold { get; set; }

        public SettingsDb()
        {
            var executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.GetDirectoryName(executablePath);

            dbPath = Path.Combine(directoryPath, dbFilename);
            if (File.Exists(dbPath))
            {
                var lines = File.ReadAllLines(dbPath);
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (trimmed.IndexOf(separator) == -1)
                    {
                        continue;
                    }

                    var split = trimmed.Split(new[] { separator });

                    switch (split[0])
                    {
                        case nameof(ConfidenceThreshold):
                            {
                                ConfidenceThreshold = Int32.Parse(split[1]);
                                break;
                            }
                    }
                }
            }
            else
            {
                ConfidenceThreshold = 50;

                SaveDb();
            }
        }

        public void SaveDb()
        {
            var db = new Dictionary<string, string>();

            db.Add(nameof(ConfidenceThreshold), ConfidenceThreshold.ToString());

            var contents = "";
            foreach (var item in db)
            {
                contents += $"{item.Key};{item.Value}\n";
            }

            contents = contents.Trim();
            File.WriteAllText(dbPath, contents);
        }
    }
}
