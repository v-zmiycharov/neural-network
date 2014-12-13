using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    public class InputItem
    {
        public double[] Attributes { get; set; }
        public int[] OutputValue { get; set; }

        public Neuron[] MiddleLayer { get; set; }
        public Neuron[] OutputLayer { get; set; }

        public InputItem()
        {
            this.Attributes = new double[Constants.ATTRIBUTES_COUNT];

            this.MiddleLayer = new Neuron[Constants.MIDDLE_LAYER_NEURONS_COUNT];

            this.OutputLayer = new Neuron[Constants.OUTPUT_LAYER_NEURONS_COUNT];
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

            for (int i = 0; i < this.OutputLayer.Length; i++)
            {
                this.OutputLayer[i] = new Neuron();

                double sum = Enumerable.Range(0, Constants.MIDDLE_LAYER_NEURONS_COUNT)
                    .Sum(start => Roads.GetLast(start, i) * this.MiddleLayer[start].Value);
                this.OutputLayer[i].Value = (double)(1 / (1 + Math.Exp(-sum)));
            }
        }

        public void CalculateLayerMistakes()
        {
            for (int i = 0; i < this.OutputLayer.Length; i++)
            {
                double newValue = this.OutputLayer[i].Value;
                this.OutputLayer[i].Mistake = newValue * (1 - newValue) * (this.OutputValue[i] - newValue);
            }

            for (int i = 0; i < this.MiddleLayer.Length; i++)
            {
                double newValue = this.MiddleLayer[i].Value;

                double sum = Enumerable.Range(0, Constants.OUTPUT_LAYER_NEURONS_COUNT)
                    .Sum(outIndex => Roads.GetLast(i, outIndex) * this.OutputLayer[outIndex].Mistake);

                this.MiddleLayer[i].Mistake = newValue * (1 - newValue) * sum;
            }
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
                for (int j = 0; j < Constants.OUTPUT_LAYER_NEURONS_COUNT; j++)
                {
                    double currentValue = Roads.GetLast(i, j);
                    Roads.SetLast(i, j, currentValue + Constants.CALCULATION_M * this.OutputLayer[j].Mistake * this.MiddleLayer[i].Value);
                }
            }
        }

        public string ToDebugString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Join("; ", this.Attributes.Select(e => e.ToString("0.0"))));
            sb.Append(" ----> ");
            for (int i = 0; i < this.OutputLayer.Length; i++)
            {
                bool isBigggest = true;
                for (int j = i + 1; j < this.OutputLayer.Length; j++)
                {
                    if(this.OutputLayer[j].Value > this.OutputLayer[i].Value)
                    {
                        isBigggest = false;
                        break;
                    }
                }
                if(isBigggest)
                {
                    sb.Append((i + 1).ToString());
                    break;
                }
            }
            sb.Append(String.Format(" ({0}; {1}; {2})", this.OutputLayer[0].Value.ToString("0.00"), this.OutputLayer[1].Value.ToString("0.00"), this.OutputLayer[2].Value.ToString("0.00")));
                
            return sb.ToString();
        }
    }
}
