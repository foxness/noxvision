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
        //background, aeroplane, bicycle, bird, boat,
        //bottle, bus, car, cat, chair, cow, diningtable,
        //dog, horse, motorbike, person, pottedplant, sheep,
        //sofa, train, tvmonitor

        private static readonly string dbFilename = "settings";
        private static readonly char separator = ';';

        private static string dbPath;

        public int ConfidenceThreshold { get; set; }
        public int FaceConfidenceThreshold { get; set; }
        public bool DrawPerson { get; set; }
        public bool DrawBackground { get; set; }
        public bool DrawAeroplane { get; set; }
        public bool DrawBicycle { get; set; }
        public bool DrawBird { get; set; }
        public bool DrawBoat { get; set; }
        public bool DrawBottle { get; set; }
        public bool DrawBus { get; set; }
        public bool DrawCar { get; set; }
        public bool DrawCat { get; set; }
        public bool DrawChair { get; set; }
        public bool DrawCow { get; set; }
        public bool DrawDiningtable { get; set; }
        public bool DrawDog { get; set; }
        public bool DrawHorse { get; set; }
        public bool DrawMotorbike { get; set; }
        public bool DrawPottedplant { get; set; }
        public bool DrawSheep { get; set; }
        public bool DrawSofa { get; set; }
        public bool DrawTrain { get; set; }
        public bool DrawTv { get; set; }

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

                        case nameof(FaceConfidenceThreshold):
                            {
                                FaceConfidenceThreshold = Int32.Parse(split[1]);
                                break;
                            }

                        case nameof(DrawPerson): { DrawPerson = Boolean.Parse(split[1]); break; }
                        case nameof(DrawBackground): { DrawBackground = Boolean.Parse(split[1]); break; }
                        case nameof(DrawAeroplane): { DrawAeroplane = Boolean.Parse(split[1]); break; }
                        case nameof(DrawBicycle): { DrawBicycle = Boolean.Parse(split[1]); break; }
                        case nameof(DrawBird): { DrawBird = Boolean.Parse(split[1]); break; }
                        case nameof(DrawBoat): { DrawBoat = Boolean.Parse(split[1]); break; }
                        case nameof(DrawBottle): { DrawBottle = Boolean.Parse(split[1]); break; }
                        case nameof(DrawBus): { DrawBus = Boolean.Parse(split[1]); break; }
                        case nameof(DrawCar): { DrawCar = Boolean.Parse(split[1]); break; }
                        case nameof(DrawCat): { DrawCat = Boolean.Parse(split[1]); break; }
                        case nameof(DrawChair): { DrawChair = Boolean.Parse(split[1]); break; }
                        case nameof(DrawCow): { DrawCow = Boolean.Parse(split[1]); break; }
                        case nameof(DrawDiningtable): { DrawDiningtable = Boolean.Parse(split[1]); break; }
                        case nameof(DrawDog): { DrawDog = Boolean.Parse(split[1]); break; }
                        case nameof(DrawHorse): { DrawHorse = Boolean.Parse(split[1]); break; }
                        case nameof(DrawMotorbike): { DrawMotorbike = Boolean.Parse(split[1]); break; }
                        case nameof(DrawPottedplant): { DrawPottedplant = Boolean.Parse(split[1]); break; }
                        case nameof(DrawSheep): { DrawSheep = Boolean.Parse(split[1]); break; }
                        case nameof(DrawSofa): { DrawSofa = Boolean.Parse(split[1]); break; }
                        case nameof(DrawTrain): { DrawTrain = Boolean.Parse(split[1]); break; }
                        case nameof(DrawTv): { DrawTv = Boolean.Parse(split[1]); break; }
                    }
                }
            }
            else
            {
                SetDefaultSettings();
                SaveDb();
            }
        }

        private void SetDefaultSettings()
        {
            ConfidenceThreshold = 50;
            FaceConfidenceThreshold = 50;
            DrawPerson = true;
            DrawBackground = true;
            DrawAeroplane = true;
            DrawBicycle = true;
            DrawBird = true;
            DrawBoat = true;
            DrawBottle = true;
            DrawBus = true;
            DrawCar = true;
            DrawCat = true;
            DrawChair = true;
            DrawCow = true;
            DrawDiningtable = true;
            DrawDog = true;
            DrawHorse = true;
            DrawMotorbike = true;
            DrawPottedplant = true;
            DrawSheep = true;
            DrawSofa = true;
            DrawTrain = true;
            DrawTv = true;
    }

        public void SaveDb()
        {
            var db = new Dictionary<string, string>();

            db.Add(nameof(ConfidenceThreshold), ConfidenceThreshold.ToString());
            db.Add(nameof(FaceConfidenceThreshold), FaceConfidenceThreshold.ToString());

            db.Add(nameof(DrawPerson), DrawPerson.ToString());
            db.Add(nameof(DrawBackground), DrawBackground.ToString());
            db.Add(nameof(DrawAeroplane), DrawAeroplane.ToString());
            db.Add(nameof(DrawBicycle), DrawBicycle.ToString());
            db.Add(nameof(DrawBird), DrawBird.ToString());
            db.Add(nameof(DrawBoat), DrawBoat.ToString());
            db.Add(nameof(DrawBottle), DrawBottle.ToString());
            db.Add(nameof(DrawBus), DrawBus.ToString());
            db.Add(nameof(DrawCar), DrawCar.ToString());
            db.Add(nameof(DrawCat), DrawCat.ToString());
            db.Add(nameof(DrawChair), DrawChair.ToString());
            db.Add(nameof(DrawCow), DrawCow.ToString());
            db.Add(nameof(DrawDiningtable), DrawDiningtable.ToString());
            db.Add(nameof(DrawDog), DrawDog.ToString());
            db.Add(nameof(DrawHorse), DrawHorse.ToString());
            db.Add(nameof(DrawMotorbike), DrawMotorbike.ToString());
            db.Add(nameof(DrawPottedplant), DrawPottedplant.ToString());
            db.Add(nameof(DrawSheep), DrawSheep.ToString());
            db.Add(nameof(DrawSofa), DrawSofa.ToString());
            db.Add(nameof(DrawTrain), DrawTrain.ToString());
            db.Add(nameof(DrawTv), DrawTv.ToString());

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
