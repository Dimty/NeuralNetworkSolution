using System.Reflection;

namespace Neuron
{
    public class Network
    {
        private List<Neuron[]> _list;

        public Network(params int[] size)
        {
            _list = new List<Neuron[]>();
            Init(size);
        }

        private void Init(int[] size)
        {
            for (int i = 0; i < size.Length; i++)
            {
                var layer = new Neuron[size[i]];
                for (int j = 0; j < size[i]; j++)
                {
                    int type;
                    type = i == 0 ? -1 : i == size.Length - 1 ? 1 : 0;
                    int s = i != 0 ? size[i - 1] : 0;
                    layer[j] = new Neuron(type, s);
                }

                _list.Add(layer);
            }
        }


        public double Train(double[,] inputArray, double[,] expectedArray, double alpha = 0.5, int epochs = 10000)
        {
            
            var error = 0.0d;
            while (epochs-- > 0)
            {
                for (int i = 0; i < inputArray.GetLength(0); i++)
                {
                    var inp = GetRow(inputArray, i);
                    var exp = GetRow(expectedArray, i);
                    FeedForward(inp);
                    error = Backpropagation(exp);
                    UpdateWeight(alpha);
                }
            }
            return error;
        }
        

        private void UpdateWeight(double alpha)
        {
            for (int layer = 1; layer < _list.Count; layer++)
            {
                for (int neuron = 0; neuron < _list[layer].Length; neuron++)
                {
                    for (int preNeuronLayer = 0; preNeuronLayer < _list[layer - 1].Length; preNeuronLayer++)
                    {
                        var delta = _list[layer][neuron].Delta;
                        var output = _list[layer - 1][preNeuronLayer].Output;
                        _list[layer][neuron][preNeuronLayer] -= alpha * delta * output;
                    }
                }
            }
        }

        private double Backpropagation(double[] exp)
        {
            double error = 0.0d;
            int indexLastLayer = _list.Count - 1;
            int sizeLastLayer = _list.Last().Length;
            for (int i = 0; i < sizeLastLayer; i++)
            {
                var exception = _list[indexLastLayer][i].Output - exp[i];
                error += exception * exception / 2;
                _list[indexLastLayer][i].Delta = exception * _list[indexLastLayer][i].Derivative;
            }

            for (int layer = indexLastLayer; layer > 1; layer--)
            {
                for (int neuron = 0; neuron < _list[layer - 1].Length; neuron++)
                {
                    var sum = 0.0d;
                    for (int neuronNextLayer = 0; neuronNextLayer < _list[layer].Length; neuronNextLayer++)
                    {
                        var delta = _list[layer][neuronNextLayer].Delta;
                        var weight = _list[layer][neuronNextLayer][neuron];
                        sum += delta * weight;
                    }
                    _list[layer - 1][neuron].Delta = sum;
                    _list[layer - 1][neuron].Delta *= _list[layer - 1][neuron].Derivative;
                }
            }

            return error;
        }

        public Neuron[] FeedForward(double[] input)
        {
            FeedFirstLayer(input);
            FeedOtherLayer();
            return _list.Last();
        }

        private void FeedOtherLayer()
        {
            for (int layer = 1; layer < _list.Count; layer++)
            {
                for (int neuron = 0; neuron < _list[layer].Length; neuron++)
                {
                    var sum = 0.0d;
                    for (int preLayer = 0; preLayer < _list[layer - 1].Length; preLayer++)
                    {
                        var weight = _list[layer][neuron][preLayer];
                        var inp = _list[layer - 1][preLayer].Output;
                        sum += inp * weight;
                    }
                    _list[layer][neuron].Input = sum;
                    _list[layer][neuron].Output = sum;
                }
            }
        }

        private void FeedFirstLayer(double[] input)
        {
            for (int i = 0; i < _list[0].Length; i++)
            {
                _list[0][i].Output = input[i];
            }
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

        public override string ToString()
        {
            return base.ToString();
            string res = "";
            for (int j = 1; j < _list.Count; j++)
            {
                res += "Layer = " + j + Environment.NewLine;
                for (int k = 0; k < _list[j].Length; k++)
                {
                    res += "Neuron = " + k + Environment.NewLine;
                    for (int i = 0; i < _list[j - 1].Length; i++)
                    {
                        res += _list[j][k][i] + Environment.NewLine;
                    }
                }
            }

            return res;
        }
    }

    public static class Helper
    {
        public static double[,] DoublesToDoubles(this double[][] param)
        {
            var x = param[0].Length;
            var y = param.Length;
            var result = new double[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    result[i, j] = param[i][j];
                }
            }

            return result;
        }

        public static double[,] IntToDoubles(this int[][] param)
        {
            var x = param[0].Length;
            var y = param.Length;
            var result = new double[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    result[i, j] = Convert.ToInt32(param[i][j]);
                }
            }

            return result;
        }
    }
}