using System;
using System.Numerics;
using FFT;
using Mesh;
using UnityEngine;
using Noise;
using Types = Noise.Types;
using Utilities;


public class TerrainGenerator : MonoBehaviour
{
    [Header("Noise Generation")]
    public Types noiseType;
    [Header("Gaussian Noise")]
    public double mean;
    public double stdDev = 1;
        
    [Header("White Noise")]
    public float scale;
        
    [Space]
    [Header("Terrain Size")]
    [Tooltip("Must be a power of 2!")]
    public int mapSize;
    public float mapScale = 1;

    [Space]
    [Header("Power Law Filter")]
    [Range(0f,3f)]
    public double alpha;

    [SerializeField] private MeshGenerator meshGeneratorNormal;
    [SerializeField] private MeshGenerator meshGeneratorWireframe;

    private void Start()
    {

    }

    public void GenerateTerrain()
    {
        // Generate the noise grid based on user input
        double[,] noiseGrid = noiseType switch
        {
            Types.Gaussian => Gaussian.GenerateGaussianNoiseGrid(mapSize, mean, stdDev),
            Types.White => White.GenerateTileableWhiteNoiseGrid(mapSize, scale),
            _ => throw new ArgumentOutOfRangeException()
        };

        // Convert to complex numbers to use in the FFT algorithm
        Complex[,] data = Utils.ConvertRealToComplex(noiseGrid, mapSize);
            
        // Apply Fast Fourier transform (FFT)
        FastFourierTransform.FFT(data);

        // Shift the data
        var shiftData = Utils.Shift(data);

        // Apply power law filter
        var filteredData = Filter.ApplyPowerLawFilter(shiftData, alpha);
            
        // Reverse the shift
        var unShiftedData = Utils.Shift(filteredData);
            
        // Apply Inverse Fast Fourier Transform (IFFT)
        FastFourierTransform.IFFT(unShiftedData);
            
        // Convert the filtered data back into real numbers to be used in the mesh generation
        float[,] noiseMap = Utils.ConvertComplexToReal(unShiftedData);
        
        // Generate the terrain mesh
        meshGeneratorNormal.GenerateTerrain(noiseMap,mapSize, mapScale);
        meshGeneratorWireframe.GenerateTerrain(noiseMap,mapSize, mapScale);
    }

}