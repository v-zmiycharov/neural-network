using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Roads.Initiate();
            Helpers.ReadTrainFile();

            Helpers.TrainItems = Helpers.TrainItems.OrderBy(e => Guid.NewGuid()).ToList();

            for (int i = 0; i < Constants.LEARNING_ITERATIONS_COUNT; i++)
            {
                for (int j = 0; j < Helpers.TrainItems.Count; j++)
                {
                    Helpers.TrainItems[j].CalculateLayerValues();
                    Helpers.TrainItems[j].CalculateLayerMistakes();
                    Helpers.TrainItems[j].UpdateRoads();
                }
            }

            Helpers.ReadTestFile();

            Console.ReadKey();
        }
    }
}
