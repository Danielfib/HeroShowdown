using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension
{
    
    public static T GetNextArrayElement<T>(this T[] array, int currentIndex)
    {
        int i = currentIndex;
        if (currentIndex + 1 >= array.Length)
        {
            i = 0;
        }
        else
        {
            i++;
        }

        return array[i];
    }

    public static T GetPreviousArrayElement<T>(this T[] array, int currentIndex)
    {
        int i = currentIndex;
        if (currentIndex + 1 >= array.Length)
        {
            i = 0;
        }
        else
        {
            i++;
        }

        return array[i];
    }


    public static int GetNextIndexOfArray(this object[] array, int currentIndex)
    {
        int i = currentIndex;
        if(currentIndex + 1 >= array.Length)
        {
            i = 0;
        } else
        {
            i++;
        }

        return i;
    }

    public static int GetPreviousIndexOfArray(this object[] array, int currentIndex)
    {
        int i = currentIndex;
        if (currentIndex - 1 < 0)
        {
            i = array.Length - 1;
        }
        else
        {
            i--;
        }

        return i;
    }
}
