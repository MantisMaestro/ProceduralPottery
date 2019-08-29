using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProcMug
{
    public static Mesh GenerateMug(float radius, float thickness, float height, int segmentCount, int handleSegmentCount)
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.name = "Mug";

        List<Vector3> vertices = new List<Vector3>();

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            float angle = t * MathUtils.TAU;

            Vector2 dir2D = MathUtils.GetVectorByAngle(angle);
            Vector3 dir = new Vector3(dir2D.x, 0f, dir2D.y);

            Vector3 p0 = radius * dir;
            Vector3 p1 = radius * dir + Vector3.up * height;
            Vector3 p2 = (radius - thickness) * dir + Vector3.up * height;
            Vector3 p3 = (radius - thickness) * dir + Vector3.up * thickness;

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);
        }

        List<int> triangles = new List<int>();

        for (int i = 0; i < segmentCount; i++)
        {
            int bottomRoot = 0;
            int bottomP2 = (bottomRoot + (4 * i)) % vertices.Count;
            int bottomP3 = (bottomRoot + (4 * i) + 4) % vertices.Count;

            int topRoot = 3;
            int topP2 = (topRoot + (4 * i)) % vertices.Count;
            int topP3 = (topRoot + (4 * i) + 4) % vertices.Count;

            //BottomBase
            triangles.Add(bottomRoot);
            triangles.Add(bottomP2);
            triangles.Add(bottomP3);

            //TopBase
            triangles.Add(topRoot);
            triangles.Add(topP3);
            triangles.Add(topP2);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        return mesh;
    }
}
