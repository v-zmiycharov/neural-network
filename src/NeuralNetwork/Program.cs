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

            Console.ReadKey();
        }
    }
}
