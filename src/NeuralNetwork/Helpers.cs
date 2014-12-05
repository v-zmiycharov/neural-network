using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    public static class Helpers
    {
        public static List<InputItem> TrainItems;

        public static void ReadTrainFile()
        {
            TrainItems = new List<InputItem>();

            string line;

            string projectDir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            StreamReader file = new StreamReader(projectDir + "\\iris_train.txt");
            while ((line = file.ReadLine()) != null)
            {
                var attributes = line.Split(',');
                if (attributes != null && attributes.Count() == Constants.ATTRIBUTES_COUNT + 1)
                {
                    var item = new InputItem();

                    for (int i = 0; i < Constants.ATTRIBUTES_COUNT; i++)
                    {
                        item.Attributes[i] = Double.Parse(attributes[i], CultureInfo.InvariantCulture);
                    }
                    item.Value = Int32.Parse(attributes[Constants.ATTRIBUTES_COUNT]);

                    TrainItems.Add(item);
                }
            }

            file.Close();
        }


        public static double GetRandom(double min, double max)
        {
            lock (syncLock)
            {
                return rnd.NextDouble() * (max - min) + min;
            }
        }

        private static readonly Random rnd = new Random();
        private static readonly object syncLock = new object();
    }
}
