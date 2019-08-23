using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public const float TAU = 6.28318530718f;

    public static Vector2 GetVectorByAngle(float angle)
    {
        return new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            );
    }
}
