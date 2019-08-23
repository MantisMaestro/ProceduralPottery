using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGeo : MonoBehaviour
{

    private void Awake()
    {

        Mesh mesh = new Mesh();
        mesh.name = "ProcGen Mesh";

        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-1, 0, 1),
            new Vector3( 1, 0, 1),
            new Vector3(-1, 0,-1),
            new Vector3( 1, 0,-1)
        };

        List<int> triangles = new List<int>()
        {
            0, 1, 2,
            1, 3, 2
        };

        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };


        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.SetNormals(normals);

        GetComponent<MeshFilter>().mesh = mesh;

    }
}
