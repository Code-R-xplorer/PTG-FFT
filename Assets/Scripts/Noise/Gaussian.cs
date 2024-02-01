using System;

namespace Noise
{
    public static class Gaussian
    {
        private static Random random = new();
        
        // Generates a random number with Gaussian distribution
        private static double GenerateGaussianNoise(double mean, double stdDev)
        {
            double u1 = 1.0 - random.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }

        public static double[,] GenerateGaussianNoiseGrid(int mapSize ,double mean, double stdDev)
        {
            
            double[,] noiseMap = new double[mapSize, mapSize];

            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    noiseMap[x, y] = GenerateGaussianNoise(mean, stdDev);
                }
            }
            
            return noiseMap;
        }

    }
}