using System;
using System.Linq;
using UnityEngine;

internal class GameTools
{
    /// <summary>
    /// Oscillate between the two values at speed.
    /// </summary>
    public static float PingPong(float min, float max, float speed = 1f)
    {
        return Mathf.PingPong(Time.time * speed, max - min) + min;
    }

    /// <summary>
    /// Wrap value between min and max values.
    /// </summary>
    public static int Wrap(int value, int min, int max)
    {
        int newValue = value % max;
        if (newValue < min) newValue = max - 1;
        return newValue;
    }

    /// <summary>
    /// Function to generate a random integer number (No Duplicates).
    /// </summary>
    public static int RandomUnique(int min, int max, int last)
    {
        System.Random rnd = new System.Random();

        if (min + 1 < max)
        {
            return Enumerable.Range(min, max).OrderBy(x => rnd.Next()).Where(x => x != last).Take(1).Single();
        }
        else
        {
            return min;
        }
    }
}