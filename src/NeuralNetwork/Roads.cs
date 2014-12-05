using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    public static class Roads
    {
        private static double[][] InitialRoads { get; set; }

        private static double[] LastRoads { get; set; }

        public static void Initiate()
        {
            InitialRoads = new double[Constants.ATTRIBUTES_COUNT][];
            for (int i = 0; i < InitialRoads.Length; i++)
            {
                InitialRoads[i] = new double[Constants.MIDDLE_LAYER_NEURONS_COUNT];
                for (int j = 0; j < InitialRoads[i].Length; j++)
                {
                    InitialRoads[i][j] = Helpers.GetRandom(Constants.INITIAL_SETUP_MIN, Constants.INITIAL_SETUP_MAX);
                }
            }

            LastRoads = new double[Constants.MIDDLE_LAYER_NEURONS_COUNT];
            for (int i = 0; i < LastRoads.Length; i++)
            {
                LastRoads[i] = Helpers.GetRandom(Constants.INITIAL_SETUP_MIN, Constants.INITIAL_SETUP_MAX);
            }
        }

        public static double GetInitial(int i, int j)
        {
            return InitialRoads[i][j];
        }

        public static double GetLast(int i)
        {
            return LastRoads[i];
        }

        public static void SetInitial(int i, int j, double value)
        {
            InitialRoads[i][j] = value;
        }

        public static void SetLast(int i, double value)
        {
            LastRoads[i] = value;
        }

    }
}
