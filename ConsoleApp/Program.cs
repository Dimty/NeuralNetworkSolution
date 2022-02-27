using System;
using Neuron;

namespace NeuralNetworkSolution
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = new double[][]
            {
                new[] {0.0, 0.0, 0.0},
                new[] {0.0, 0.0, 1.0},
                new[] {0.0, 1.0, 0.0},
                new[] {0.0, 1.0, 1.0},
                new[] {1.0, 0.0, 0.0},
                new[] {1.0, 0.0, 1.0},
                new[] {1.0, 1.0, 0.0},
                new[] {1.0, 1.0, 1.0},
            };
            var output = new double[,]
            {
                {0,1},
                {0,1},
                {1,0},
                {1,0},
                {0,1},
                {0,1},
                {1,0},
                {1,0},
                
            };
            
            Network network = new Network(input[0].Length,4,output.GetLength(1));
            network.Train(input.DoublesToDoubles(),output);
            
            for (int i = 0; i < input.Length; i++)
            {
                var res = network.FeedForward(input[i]);
                Console.WriteLine("{0} {1} {2} res {3:F0}",input[i][0],input[i][1],input[i][2],res[0].Output);
            }

            Console.WriteLine("----------------------------------------------------------");
            for (int i = 0; i < input.Length; i++)
            {
                var res = network.FeedForward(input[i]);
                Console.WriteLine("{0} {1} {2} res {3:F0}",input[i][0],input[i][1],input[i][2],res[1].Output);
            }
            //Console.WriteLine(network);
            #region MyWorkVersion




            // var input = new int[][]
            // {
            //     new[] {0, 0, 0},
            //     new[] {0, 0, 1},
            //     new[] {0, 1, 0},
            //     new[] {0, 1, 1},
            //     new[] {1, 0, 0},
            //     new[] {1, 0, 1},
            //     new[] {1, 1, 0},
            //     new[] {1, 1, 1},
            // };
            // var output = new int[,]
            // {
            //     {0,1},
            //     {0,1},
            //     {1,0},
            //     {1,0},
            //     {0,1},
            //     {0,1},
            //     {1,0},
            //     {1,0},
            // };
            //
            // var sig0 = 0.0d;
            // var sig1 = 0.0d;
            //
            // var der0 = 0.0d;
            // var der1 = 0.0d;
            //
            // var sum0 = 0.0d;
            // var sum1 = 0.0d;
            //
            // var delta0 = 0.0d;
            // var delta1 = 0.0d;
            //
            // var alpha = 0.5;
            //
            // var weight00 = new Random().NextDouble() - 0.5;
            // var weight01 = new Random().NextDouble() - 0.5;
            // var weight02 = new Random().NextDouble() - 0.5;
            //
            // var weight10 = new Random().NextDouble() - 0.5;
            // var weight11 = new Random().NextDouble() - 0.5;
            // var weight12 = new Random().NextDouble() - 0.5;
            //
            // int epoch = 10000;
            // while (epoch-- > 0)
            // {
            //     int count = 0;
            //     foreach (var item in input)
            //     {
            //         #region neuron1
            //
            //         sum0 = item[0] * weight00 + item[1] * weight01 + item[2] * weight02;
            //         sig0 = 1 / (1 + Math.Exp(-sum0));
            //         der0 = sig0 * (1 - sig0);
            //         delta0 = sig0 - output[count,0];
            //
            //         #endregion
            //
            //         #region neuron2
            //
            //         sum1 = item[1] * weight10 + item[1] * weight11 + item[2] * weight12;
            //         sig1 = 1 / (1 + Math.Exp(-sum1));
            //         der1 = sig1 * (1 - sig1);
            //         delta1 = sig1 - output[count,1];
            //         
            //         #endregion
            //
            //         var sumDelta = delta0 + delta1;
            //
            //
            //         weight00 -= alpha * delta0 * der0 * item[0];
            //         weight01 -= alpha * delta0 * der0 * item[1];
            //         weight02 -= alpha * delta0 * der0 * item[2];
            //
            //         weight10 -= alpha * delta1 * der1 * item[0];
            //         weight11 -= alpha * delta1 * der1 * item[1];
            //         weight12 -= alpha * delta1 * der1 * item[2];
            //         
            //         count++;
            //     }
            // }
            //
            // Console.WriteLine("Neuron 1");
            // foreach (var item in input)
            // {
            //     var res = item[0] * weight00 + item[1] * weight01 + item[2] * weight02;
            //     res = 1 / (1 + Math.Exp(-res));
            //     Console.WriteLine("{0} {1} {2} res {3:F0}", item[0], item[1], item[2], res);
            //     
            // }
            //
            // Console.WriteLine("Neuron 2");
            // foreach (var item in input)
            // {
            //     var res = item[0] * weight10 + item[1] * weight11 + item[2] * weight12;
            //     res = 1 / (1 + Math.Exp(-res));
            //     Console.WriteLine("{0} {1} {2} res {3:F0}", item[0], item[1], item[2], res);
            // }
            //
            // Console.WriteLine(weight00);
            // Console.WriteLine(weight01);
            // Console.WriteLine(weight02);
            // Console.WriteLine();
            // Console.WriteLine(weight10);
            // Console.WriteLine(weight11);
            // Console.WriteLine(weight12);

            #endregion

            #region workVersion

//             // массив входных обучающих векторов
//             Vector[] X = {
//                 new Vector(0, 0),
//                 new Vector(0, 1),
//                 new Vector(1, 0),
//                 new Vector(1, 1)
//             };
//
// // массив выходных обучающих векторов
//             Vector[] Y = {
//                 new Vector(0.0), // 0 ^ 0 = 0
//                 new Vector(1.0), // 0 ^ 1 = 1
//                 new Vector(1.0), // 1 ^ 0 = 1
//                 new Vector(0.0) // 1 ^ 1 = 0
//             };
//
//             Network network = new Network(new int[]{2, 3, 1}); // создаём сеть с двумя входами, тремя нейронами в скрытом слое и одним выходом
//             network.Train(X, Y, 0.5, 1e-7, 100000); // запускаем обучение сети 
//             for (int i = 0; i < 4; i++) {
//                 Vector output = network.Forward(X[i]);
//                 Console.WriteLine("X: {0} {1}, Y: {2}, output: {3}", X[i][0], X[i][1], Y[i][0], output[0]);
//             }

            #endregion

            #region New

            // var input = new double[,]
            // {
            //     {0,0,0},
            //     {0,0,1},
            //     {0,1,0},
            //     {0,1,1},
            //     {1,0,0},
            //     {1,0,1},
            //     {1,1,0},
            //     {1,1,1},
            // };
            // var output = new double[,]
            // {
            //     {0},
            //     {0},
            //     {0},
            //     {0},
            //     {1},
            //     {1},
            //     {1},
            //     {1},
            // };
            // Neuron.Network network = new Neuron.Network(3,4,1);
            // network.Train(input, output);
            // for (int i = 0; i < 2; i++)
            // {
            //     for (int j = 0; j < 2; j++)
            //     {
            //         for (int k = 0; k < 2; k++)
            //         {
            //             var res = network.FeedForward(new double[] {i, j,k});
            //             Console.WriteLine(res[0].Output);
            //         }
            //     }
            // }

            #endregion
        }
    }
}