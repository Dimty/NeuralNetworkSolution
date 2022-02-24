using System.Drawing;

namespace Neuron
{
    public class Weight
    {
        private int _amountOfNeuronPreLayer = 0;
        private int _amountOfNeuronActLayer = 0;
        private double[] _dictionary;
        public int Count => _dictionary.Length;

        public Weight(int preLayer, int actLayer)
        {
            _amountOfNeuronPreLayer = preLayer;
            _amountOfNeuronActLayer = actLayer;
            _dictionary = new double[preLayer * actLayer];
            InitWeights(new Random().Next() & 0x7FFFFFFF);
        }


        public double GetWeightValue(int a, int b)
        {
            return _dictionary[a + b * _amountOfNeuronPreLayer];
        }

        private void InitWeights(int seed)
        {
            Random rnd = new Random(seed);
            for (int i = 0; i < _dictionary.Length; i++)
            {
                _dictionary[i] = rnd.NextDouble()-0.5; // [-0.5,0.5)
            }
        }
    }
}