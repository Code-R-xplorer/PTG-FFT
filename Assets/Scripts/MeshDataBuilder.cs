/*************************************************************************************************
     *    Title: MeshDataBuilder
     *    Author:  Arturo Nereu 
     *    Date: 2020
     *    Link: https://github.com/ArturoNereu/WireframeShaderGraph
     *    License: MIT https://github.com/ArturoNereu/WireframeShaderGraph?tab=MIT-1-ov-file
     *    Notes: Code has been modified to suit the needs of the project
**************************************************************************************************/

// Based on: https://catlikecoding.com/unity/tutorials/advanced-rendering/flat-and-wireframe-shading/

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDataBuilder : MonoBehaviour
{
    /// <summary>
    /// We will assign a color to each Vertex in a Triangle on the object's mesh
    /// </summary>
    public void GenerateMeshData(UnityEngine.Mesh mesh)
    {
        SplitMesh(mesh);
        SetVertexColors(mesh);
    }

    /// <summary>
    /// For this approach, we need to make sure there are not shared vertices
    /// on the mesh, that's why we use this method to split the mesh. 
    /// This will increase the number of vertices, so less optimized.
    /// </summary>
    /// <param name="mesh"></param>
    private static void SplitMesh(UnityEngine.Mesh mesh)
    {
        var meshTriangles = mesh.triangles;
        var meshVertices = mesh.vertices;
        var meshNormals = mesh.normals;
        var meshUV = mesh.uv;

        var n = meshTriangles.Length;
        var newVertices = new Vector3[n];
        var newNormals = new Vector3[n];
        var newUvs = new Vector2[n];

        for (var i = 0; i < n; i++)
        {
            newVertices[i] = meshVertices[meshTriangles[i]];
            newNormals[i] = meshNormals[meshTriangles[i]];
            if (meshUV.Length > 0)
            {
                newUvs[i] = meshUV[meshTriangles[i]];
            }
            meshTriangles[i] = i;
        }

        mesh.vertices = newVertices;
        mesh.normals = newNormals;
        mesh.uv = newUvs;
        mesh.triangles = meshTriangles;
    }

    /// <summary>
    /// We paint the vertex color
    /// </summary>
    /// <param name="mesh"></param>
    private static void SetVertexColors(UnityEngine.Mesh mesh)
    {
        var colorCoords = new[]
        {
            new Color(1, 0, 0),
            new Color(0, 1, 0),
            new Color(0, 0, 1),
        };

        var vertexColors = new Color32[mesh.vertices.Length];

        for (var i = 0; i < vertexColors.Length; i += 3)
        {
            vertexColors[i] = colorCoords[0];
            vertexColors[i + 1] = colorCoords[1];
            vertexColors[i + 2] = colorCoords[2];
        }

        mesh.colors32 = vertexColors;
    }
}