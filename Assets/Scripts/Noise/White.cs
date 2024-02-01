namespace Noise
{
    public static class White
    {
        public static double[,] GenerateTileableWhiteNoiseGrid(int mapSize, float scale)
        {
            double[,] noiseMap = new double[mapSize, mapSize];

            // Generate internal noise
            for (int y = 0; y < mapSize - 1; y++)
            {
                for (int x = 0; x < mapSize - 1; x++)
                {
                    noiseMap[x, y] = UnityEngine.Random.Range(-1f, 1f) / scale;
                }
            }

            // Mirror the edges
            for (int y = 0; y < mapSize; y++)
            {
                noiseMap[mapSize - 1, y] = noiseMap[0, y]; // Mirror right edge to the left edge
            }

            for (int x = 0; x < mapSize; x++)
            {
                noiseMap[x, mapSize - 1] = noiseMap[x, 0]; // Mirror bottom edge to the top edge
            }

            return noiseMap;
        }
    }
}