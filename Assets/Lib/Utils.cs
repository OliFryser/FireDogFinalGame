using System;
using UnityEngine;
public static class Utils
{
    public static Vector3 GetCardinalDirection(Vector2 movingDirection)
    {
        if (Math.Abs(movingDirection.x) >= Math.Abs(movingDirection.y))
        {
            // facing either left or right
            if (movingDirection.x > 0)
                return Vector3.right;
            else
                return Vector3.left;
        }
        else
        {
            // facing either up or down
            if (movingDirection.y > 0)
                return Vector3.up;
            else
                return Vector3.down;
        }
    }

    public static bool IsHorizontal(Vector2 movingDirection)
        => Math.Abs(movingDirection.x) >= Math.Abs(movingDirection.y);
}