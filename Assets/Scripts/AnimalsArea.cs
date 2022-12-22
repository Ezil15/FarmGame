using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsArea : MonoBehaviour
{
    public Transform Point1, Point2;
    private static Vector3 point1, point2;

    void Start()
    {
        point1 = Point1.position;
        point2 = Point2.position;
    }

    public static Vector3 GetRandomPoint()
    {
        return point1 + Random.Range(0f, 1f) * (point2 - point1);
    }
}
