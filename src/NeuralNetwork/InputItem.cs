using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    public class InputItem
    {
        public double[] Attributes { get; set; }
        public int Value { get; set; }

        public double[] MiddleLayer { get; set; }
        public double OutputLayer { get; set; }

        public InputItem()
        {
            this.Attributes = new double[Constants.ATTRIBUTES_COUNT];

            this.MiddleLayer = new double[Constants.MIDDLE_LAYER_NEURONS_COUNT];
        }

        public void CalculateLayerValues()
        {
            for (int i = 0; i < this.MiddleLayer.Length; i++)
            {
                double sum = Enumerable.Range(0, Constants.ATTRIBUTES_COUNT)
                    .Sum(start => Roads.GetInitial(start, i) * this.Attributes[start]);
                this.MiddleLayer[i] = (double)(1/(1+Math.Exp(-sum)));
            }

            double middleLayerSum = Enumerable.Range(0, Constants.MIDDLE_LAYER_NEURONS_COUNT)
                    .Sum(start => Roads.GetLast(start) * this.MiddleLayer[start]);
            this.OutputLayer = (double)(1 / (1 + Math.Exp(-middleLayerSum)));
        }
    }
}
