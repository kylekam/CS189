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
    center.transform.position -= transform.right * 0.012f;
    center.transform.position += transform.up * 0.005f;
    center.transform.position += transform.forward * 0.005f;

    // Find vectors corresponding to two of the sides of the triangle.
    Vector3 side1 = pointB.transform.position - pointA.transform.position;
    Vector3 side2 = pointC.transform.position - pointA.transform.position;

    Vector3 normal = Vector3.Cross(side1, side2).normalized;

    //Set center of triangle's rotation to the normal
    center.transform.rotation = Quaternion.LookRotation(normal);
    }

    
    // Finds the center along the longest side of the triangle
    private Vector3 GetCenter(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
        Vector3[] compare = new Vector3[3];
        Vector3 maxVector = Vector3.zero;
        int indexOfLargest = 0;

        compare[0] = pointA;
        compare[1] = pointB;
        compare[2] = pointC;

        for (int i = 0; i < compare.Length; i++)
        {
            Vector3 distance = compare[i] - compare[i + 1 > 2 ? 0 : i + 1];
            if (distance.magnitude > maxVector.magnitude)
            {
                maxVector = distance;
                indexOfLargest = i;
            }
        }

        maxVector = compare[indexOfLargest] - maxVector / 2;
        Debug.Log(indexOfLargest);

        return maxVector;
    }
}
