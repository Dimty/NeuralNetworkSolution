using System.Drawing;

namespace Neuron
{
    public class Weight
    {
        private Dictionary<Point, double> _dictionary;

        public Weight()
        {
            _dictionary = new();
        }

        public void AddWeight(int a,int b,double weight)
        {
            if (a < 0 || b < 0)
            {
                throw new ArgumentException("Arguments can't be less than 0");
            }
            _dictionary.Add(new Point(a,b),weight);
        }
               
    }
}