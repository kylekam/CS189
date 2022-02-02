using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NaughtyAttributes;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class TriangleCalculator : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject center;

    [Button]
    public void MakeTriangle()
    {
    //Figure out center of 3 points to a triangle
      center.transform.position = GetCenter(pointA.transform.position, pointB.transform.position, pointC.transform.position);

    // Find vectors corresponding to two of the sides of the triangle.
    Vector3 side1 = pointB.transform.position - pointA.transform.position;
    Vector3 side2 = pointC.transform.position - pointA.transform.position;

    Vector3 normal = Vector3.Cross(side1, side2).normalized;

    //Set center of triangle's rotation to the normal
    center.transform.rotation = Quaternion.LookRotation(normal);
    }

    private Vector3 GetCenter(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
      Vector3 returnVector = Vector3.zero;
      returnVector.x = (pointA.x + pointB.x + pointC.x) / 3;
      returnVector.y = (pointA.y + pointB.y + pointC.y) / 3;
      returnVector.z = (pointA.z + pointB.z + pointC.z) / 3;

    return  returnVector;
    }
}
