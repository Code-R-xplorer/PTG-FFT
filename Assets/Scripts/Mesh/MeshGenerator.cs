using UnityEngine;
using UnityEngine.Rendering;

namespace Mesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshGenerator : MonoBehaviour
    {
        public bool wireframeMesh;
        private UnityEngine.Mesh _mesh;
        
        private Vector3[] _vertices;
        private int[] _triangles;
        private Color[] _colors;

        public Gradient gradient;

        private float _minHeight;
        private float _maxHeight;

        private MeshDataBuilder _meshDataBuilder;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _mesh = new UnityEngine.Mesh
            {
                indexFormat = IndexFormat.UInt32
            };
            GetComponent<MeshFilter>().mesh = _mesh;
            if(wireframeMesh) _meshDataBuilder = GetComponent<MeshDataBuilder>();
        }

        public void GenerateTerrain(float[,] noiseMap, int size, float scale)
        {
            CreateShape(noiseMap, size, scale);
            UpdateMesh();
        }

        private void UpdateMesh()
        {
            // Clear any previous generated mesh 
            _mesh.Clear();
                
            // Add the generated vertices and triangles
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.colors = _colors;
            
            // Recalculate the mesh normals so lighting can be properly calculated
            _mesh.RecalculateNormals();
            
            if(wireframeMesh) _meshDataBuilder.GenerateMeshData(_mesh);
        }
        
        private void CreateShape(float[,] noiseMap, int size, float scale)
        {
            var offset = (size / 2) * scale;
            size--;
            _vertices = new Vector3[(size + 1) * (size + 1)];
            
            for (int i = 0, z = 0; z <= size; z++)
            {
                for (int x = 0; x <= size; x++)
                {
                    // Apply scale to x and z positions
                    float worldX = x * scale;
                    float worldZ = z * scale;

                    var y = noiseMap[x, z];
                    if (y > _maxHeight) _maxHeight = y;
                    if (y < _minHeight) _minHeight = y;
                    
                    _vertices[i] = new Vector3(worldX - offset, y, worldZ - offset);
                    i++;
                }
            }
    
            _triangles = new int[size * size * 6];

            for (int vert = 0, tris = 0, z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    // a----b
                    // |    |
                    // c----d
                    
                    _triangles[tris + 0] = vert; // c
                    _triangles[tris + 1] = vert + size + 1; // a
                    _triangles[tris + 2] = vert + 1; // d
                    _triangles[tris + 3] = vert + 1; // d
                    _triangles[tris + 4] = vert + size + 1; // a
                    _triangles[tris + 5] = vert + size + 2; // b

                    vert++;
                    tris += 6;
                }
                
                // Increase vert at the end of the row to
                // prevent the last vert of the previous row being connected
                vert++; 
            }

            if (wireframeMesh) return;

            _colors = new Color[_vertices.Length];
            for (int i = 0, z = 0; z <= size; z++)
            {
                for (int x = 0; x <= size; x++)
                {
                    float height = Mathf.InverseLerp(_minHeight, _maxHeight, _vertices[i].y);
                    _colors[i] = gradient.Evaluate(height);
                    i++;
                }
            }
        }
    }
}
