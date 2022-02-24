namespace Neuron
{
    public class Network
    {
        private Layer[] _layers;
        private Weight[] _weights;
        private List<double[]> _delta;
        public Network(params int[] size)
        {
            _layers = new Layer[size.Length];
            _weights = new Weight[size.Length-1];
            _delta = new List<double[]>();
        }

        public double Train(double[,] inputArray,double[,] expectedArray,double alfa=0.1,int epochs=10000)
        {
            var error = 0.0d;
            for (int i = 0; i < inputArray.GetLength(0); i++)
            {
                var inp = GetRow(inputArray,i);
                var exp = GetRow(expectedArray, i);
                FeedForward(inp);
                error = Backpropagation(exp);
            }

            return error;
        }

        private double Backpropagation(double[] exp)
        {
            double[] res = new double[_layers.Last().Count];
            var error = 0.0d;
            for (int i = 0; i < _layers.Last().Count; i++)
            {
                var difference = _layers[_layers.Length - 1][i].Output - exp[i];
                res[i] = difference * _layers[_layers.Length - 1][i].Derivative;
                error += difference * difference / 2;
            }
            _delta.Add(res);
            for (int layer = _layers.Length-1; layer > 0; layer--)
            {
                res = new double[_layers[layer].Count];
                for (int x = 0; x < _layers[layer].Count; x++)
                {
                    double sum = 0.0d;
                    for (int y = 0; y < _layers[layer-1].Count; y++)
                    {
                        sum += _weights[layer].GetWeightValue(y, x) * _delta.Last()[x];
                    }
                    
                }
            }

            return error;
        }

        private Layer FeedForward(double[] input)
        {
            // Init input layer
            for (int i = 0; i < _layers[0].Count; i++)
            {
                _layers[0][i].Input = input[i];
                _layers[0][i].Output = input[i];
            }
            
            // Init other layers
            for (int layer = 1; layer < _layers.Length-1; layer++)
            {
                double sum = 0.0d;
                for (int neuron = 0; neuron < _layers[layer].Count; neuron++)
                {
                    for (int nextNeuron = 0; nextNeuron < _layers[layer+1].Count; nextNeuron++)
                    {
                        sum += _layers[layer][neuron].Output * _weights[nextNeuron].GetWeightValue(nextNeuron,neuron);
                    }
                    _layers[layer][neuron].Input = sum;
                }
            }

            return _layers[^1];
        }

        private double[] GetRow(double[,] inputArray, int row)
        {
            var result = new double[inputArray.GetLength(1)];
            for (int j = 0; j < inputArray.GetLength(1); j++)
            {
                result[j] = inputArray[row, j];
            }
            return result;
        }
    }
}