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

        public Neuron[] MiddleLayer { get; set; }
        public Neuron OutputLayer { get; set; }

        public InputItem()
        {
            this.Attributes = new double[Constants.ATTRIBUTES_COUNT];

            this.MiddleLayer = new Neuron[Constants.MIDDLE_LAYER_NEURONS_COUNT];
        }

        public void CalculateLayerValues()
        {
            for (int i = 0; i < this.MiddleLayer.Length; i++)
            {
                this.MiddleLayer[i] = new Neuron();

                double sum = Enumerable.Range(0, Constants.ATTRIBUTES_COUNT)
                    .Sum(start => Roads.GetInitial(start, i) * this.Attributes[start]);
                this.MiddleLayer[i].Value = (double)(1 / (1 + Math.Exp(-sum)));
            }

            this.OutputLayer = new Neuron();

            double middleLayerSum = Enumerable.Range(0, Constants.MIDDLE_LAYER_NEURONS_COUNT)
                    .Sum(start => Roads.GetLast(start) * this.MiddleLayer[start].Value);
            this.OutputLayer.Value = (double)(1 / (1 + Math.Exp(-middleLayerSum)));
        }

        public void CalculateLayerMistakes()
        {
            for (int i = 0; i < this.MiddleLayer.Length; i++)
            {
                double newValue = this.MiddleLayer[i].Value;
                this.MiddleLayer[i].Mistake = newValue * (1 - newValue) * 1;
            }

            double calculatedValue = this.OutputLayer.Value;
            this.OutputLayer.Mistake = calculatedValue * (1 - calculatedValue) * (this.Value - calculatedValue);
        }

        public void UpdateRoads()
        {
            for (int i = 0; i < Constants.ATTRIBUTES_COUNT; i++)
            {
                for (int j = 0; j < Constants.MIDDLE_LAYER_NEURONS_COUNT; j++)
                {
                    double currentValue = Roads.GetInitial(i, j);
                    Roads.SetInitial(i, j, currentValue + Constants.CALCULATION_M * this.MiddleLayer[j].Mistake * this.Attributes[i]);
                }
            }

            for (int i = 0; i < Constants.MIDDLE_LAYER_NEURONS_COUNT; i++)
            {
                double currentValue = Roads.GetLast(i);
                Roads.SetLast(i, currentValue + Constants.CALCULATION_M * this.OutputLayer.Mistake * this.MiddleLayer[i].Value);
            }
        }
    }
}
