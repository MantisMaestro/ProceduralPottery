using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmoUtils
{
    public static void DrawWireCircle(Vector3 pos, Quaternion rot, float radius, int detail = 32)
    {
        Vector3[] points3D = new Vector3[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angleRad = t * MathUtils.TAU;

            Vector2 point2D = MathUtils.GetVectorByAngle(angleRad) * radius;

            points3D[i] = pos + rot * point2D;
        }

        for (int i = 0; i < detail; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[(i + 1) % detail]);
        }
    }
}
