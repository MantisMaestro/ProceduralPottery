using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProcPlate
{
    public struct PlateParams
    {
        public float radius;
        public float baseRadius;
        public float depth;
        public float thickness;
        public int segmentCount;
    }

    public static Mesh GeneratePlate(float radius, float baseRadius, float depth, float thickness, int segmentCount)
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.name = "Plate";

        List<Vector3> vertices = new List<Vector3>();

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            float angle = t * MathUtils.TAU;

            Vector2 dir2D = MathUtils.GetVectorByAngle(angle);
            Vector3 dir = new Vector3(dir2D.x, 0f, dir2D.y);

            vertices.Add(dir * baseRadius);
            vertices.Add(dir * radius + Vector3.up * depth);

            vertices.Add(dir * baseRadius + Vector3.up * thickness);
            vertices.Add(dir * radius + Vector3.up * thickness + Vector3.up * depth);
        }

        List<int> triangles = new List<int>();

        for (int i = 1; i <= segmentCount - 2; i++)
        {
            int bottomRoot = 0;
            int bottomP2 = (bottomRoot + 4 * i) % vertices.Count;
            int bottomP3 = (bottomRoot + 4 * i + 4) % vertices.Count;

            int topRoot = 2;
            int topP2 = (topRoot + 4 * i) % vertices.Count;
            int topP3 = (topRoot + 4 * i + 4) % vertices.Count;

            triangles.Add(bottomRoot);
            triangles.Add(bottomP2);
            triangles.Add(bottomP3);
           
            triangles.Add(topRoot);
            triangles.Add(topP3);
            triangles.Add(topP2);
        }

        for (int i = 0; i < segmentCount; i++)
        {
            int bottomInner = (i * 4) % vertices.Count;
            int bottomOuter = (i * 4 + 1) % vertices.Count;
            int topInner = (i * 4 + 2) % vertices.Count;
            int topOuter = (i * 4 + 3) % vertices.Count;

            int bottomInnerNext = (i * 4 + 4) % vertices.Count;
            int bottomOuterNext = (i * 4 + 5) % vertices.Count;
            int topInnerNext = (i * 4 + 6) % vertices.Count;
            int topOuterNext = (i * 4 + 7) % vertices.Count;

            //Bottom Outer Side
            triangles.Add(bottomInner);
            triangles.Add(bottomOuter);
            triangles.Add(bottomInnerNext);
            triangles.Add(bottomOuter);
            triangles.Add(bottomOuterNext);
            triangles.Add(bottomInnerNext);
            //Top Outer Side            
            triangles.Add(topOuterNext);
            triangles.Add(topOuter);
            triangles.Add(topInner);
            triangles.Add(topInnerNext);
            triangles.Add(topOuterNext);
            triangles.Add(topInner);
            //Outer Edge
            triangles.Add(topOuter);
            triangles.Add(bottomOuterNext);
            triangles.Add(bottomOuter);
            triangles.Add(topOuter);
            triangles.Add(topOuterNext);
            triangles.Add(bottomOuterNext);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        return mesh;
    }

}
