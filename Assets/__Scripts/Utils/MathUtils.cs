using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Vector2 ClampVectorTo8DiagonalVector(Vector2 v, float tolerance)
    {
        Vector2 clampedVector = new Vector2();

        v.Normalize();
        //Debug.Log(v);

        if(v.x > 0)
        {
            if (v.x > tolerance)
                clampedVector.x = 1;
            else
                clampedVector.x = 0;
        } else if (v.x < 0)
        {
            if (v.x < tolerance * -1)
                clampedVector.x = -1;
            else
                clampedVector.x = 0;
        } else
        {
            clampedVector.x = 0;
        }

        if(v.y > 0)
        {
            if (v.y > tolerance)
                clampedVector.y = 1;
            else
                clampedVector.y = 0;
        } else if (v.y < 0)
        {
            if (v.y < tolerance * -1)
                clampedVector.y = -1;
            else
                clampedVector.y = 0;
        }
        else
        {
            clampedVector.y = 0;
        }


        return clampedVector;
    }
}
