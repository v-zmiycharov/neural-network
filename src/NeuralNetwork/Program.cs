using System;
using System.Collections.Generic;
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

            for (int i = 0; i < Constants.LEARNING_ITERATIONS_COUNT; i++)
            {
                for (int j = 0; j < Helpers.TrainItems.Count; j++)
                {
                    Helpers.TrainItems[j].CalculateLayerValues();
                }
            }

            Console.ReadKey();
        }
    }
}
