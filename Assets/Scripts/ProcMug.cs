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

			int bottomOuter = (i * 4) % vertices.Count;
			int topOuter = (i * 4 + 1) % vertices.Count;
			int topInner = (i * 4 + 2) % vertices.Count;
			int bottomInner = (i * 4 + 3) % vertices.Count;

			int bottomOuterNext = (i * 4 + 4) % vertices.Count;
			int topOuterNext = (i * 4 + 5) % vertices.Count;
			int topInnerNext = (i * 4 + 6) % vertices.Count;
			int bottomInnerNext = (i * 4 + 7) % vertices.Count;

			//Outside
			triangles.Add(bottomOuter);
			triangles.Add(topOuter);
			triangles.Add(bottomOuterNext);
			triangles.Add(bottomOuterNext);
			triangles.Add(topOuter);
			triangles.Add(topOuterNext);

			//Top edge
			triangles.Add(topInner);
			triangles.Add(topOuterNext);
			triangles.Add(topOuter);
			triangles.Add(topOuterNext);
			triangles.Add(topInner);
			triangles.Add(topInnerNext);

			//Inside
			triangles.Add(topInner);
			triangles.Add(bottomInnerNext);
			triangles.Add(topInnerNext);
			triangles.Add(bottomInnerNext);
			triangles.Add(topInner);
			triangles.Add(bottomInner);
		}

		//Mug Handle

		Vector3 handleOrigin = new Vector3(radius - thickness / 2, height / 2, 0f);
		int vertexRoot = vertices.Count;

		for (int i = 0; i <= handleSegmentCount; i++)
		{
			float u = (i / (float)handleSegmentCount) * 0.5f;
			float angleOuter = MathUtils.TAU * u;	//The angle that this ring of vertices will be rotates about the handleOrigin.

			for (int j = 0; j < handleSegmentCount; j++)
			{
				float t = j / (float)handleSegmentCount;
				float angleInner = MathUtils.TAU * t;

				Vector2 dir2D = MathUtils.GetVectorByAngle(angleInner) * thickness * 0.5f;
				Vector3 dir = new Vector3(0f, dir2D.x, dir2D.y) + handleOrigin + Vector3.up*height*0.35f;
				dir = MathUtils.RotatePointAroundPivot(handleOrigin, dir, new Vector3(0f, 0f, angleOuter * 180/Mathf.PI));

				vertices.Add(dir);
			}
		}

		int handleVertexCount = vertices.Count - vertexRoot;

		for (int k = 0; k < handleSegmentCount; k++)
		{		
			for (int l = 0; l < handleSegmentCount; l++)
			{
				int root = (k * handleSegmentCount + l) % handleVertexCount;
				int rootAdj = (root + 1) % handleVertexCount + vertexRoot;
				int rootNext = (root + handleSegmentCount) % handleVertexCount + vertexRoot;
				int rootAdjNext = (root + handleSegmentCount + 1) % handleVertexCount + vertexRoot;
				root += vertexRoot;

				triangles.Add(root);
				triangles.Add(rootAdjNext);
				triangles.Add(rootNext);

				triangles.Add(root);
				triangles.Add(rootAdj);
				triangles.Add(rootAdjNext);
			}
		}

		mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        return mesh;
    }
}
