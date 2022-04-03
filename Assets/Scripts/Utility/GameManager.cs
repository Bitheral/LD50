using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static float time = 0;
    public static Transform earthTransform;

    public static void tick()
    {
        time += Time.deltaTime;
    }
}
