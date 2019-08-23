using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class QuadRing : MonoBehaviour
{
    [SerializeField] float innerRadius;
    [SerializeField] float thickness;

    [Range(3,256)]
    [SerializeField] int segmentCount;

    Mesh mesh;

    /*private void OnDrawGizmosSelected()
    {
        GizmoUtils.DrawWireCircle(transform.position, transform.rotation, innerRadius, segmentCount);
        GizmoUtils.DrawWireCircle(transform.position, transform.rotation, innerRadius + thickness, segmentCount);
    }*/

    void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadRing";

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void Update()
    {
        GenerateMesh();  //POTTERY
    }

    void GenerateMesh()
    {
        mesh.Clear();

        int vCount = segmentCount * 2;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            float angle = t * MathUtils.TAU;

            Vector2 dir = MathUtils.GetVectorByAngle(angle);
            Vector3 zOffset = Vector3.forward * Mathf.Cos(angle * 4);

            vertices.Add( (Vector3)dir * innerRadius + zOffset );
            vertices.Add( (Vector3)dir * (innerRadius + thickness));

            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);
        }

        List<int> triangles = new List<int>();

        for (int i = 0; i < segmentCount; i++)
        {
            int indexRoot = i * 2;
            int indexOuterRoot = indexRoot + 1;
            int indexInnerNext = (indexRoot + 2) % vCount;
            int indexOuterNext = (indexRoot + 3) % vCount;

            triangles.Add(indexRoot);
            triangles.Add(indexOuterRoot);
            triangles.Add(indexOuterNext);

            triangles.Add(indexRoot);
            triangles.Add(indexOuterNext);
            triangles.Add(indexInnerNext);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);

        //mesh.RecalculateNormals();
    }

}
