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

        public InputItem()
        {
            this.Attributes = new double[Constants.ATTRIBUTES_COUNT];
        }
    }
}
