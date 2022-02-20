using System;

namespace NeuralNetworkSolution
{
    static class Program
    {
        public static double beforeSigmoid = 0;
        public static double afterSigmoid = 0;

        public static void Main(string[] args)
        {
            var input = new int[,] {{0, 0}, {0, 1}, {1, 0}, {1, 1}};
            var output = new int[] {0, 1, 1, 1};

            double weight1 = 0.5d;
            double weight2 = 0.5d;


            for (int i = 0; i < 5000; i++)
            {
                for (int j = 0; j < input.GetLength(0); j++)
                {
                    var a = input[j, 0] * weight1;
                    var b = input[j, 1] * weight2;
                    var res = SumAndSig(a, b);
                    afterSigmoid = res;
                    var delta = output[j] - res;
                    weight1 = weight1 + delta * (res * (1-res)) * input[j, 0];
                    weight2 = weight2 + delta * (res * (1-res)) * input[j, 1];
                }
            }

            Console.WriteLine("Ready?");
            Console.WriteLine("W1 = "+weight1);
            Console.WriteLine("W2 = "+weight2);
            Console.WriteLine();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                var i1 = input[i, 0] * weight1;
                var i2 = input[i, 1] * weight2;
                var res = SumAndSig(i1, i2);
                Console.WriteLine($"For {input[i,0]} and {input[i,1]} predict equal {res}");
            }
            
        }

        static double Sig(double x)
        {
            var result = Sigmoid(x);
            return result * (1 - result);
        }

        public static double SumAndSig(double a, double b)
        {
            var result = a + b;
            beforeSigmoid = result;
            return Sigmoid(result);
        }

        private static double Sigmoid(double result)
        {
            var res = 1 / (1 + Math.Exp(-result));
            return res;
        }

        class TestNeuron
        {
        }
    }
}