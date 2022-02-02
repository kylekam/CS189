using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
  public TriangleCalculator calc;
  public Transform targetPivot;

  private int counter = -1;

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {

      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 100))
      {
        counter++;
        int moduloCounter = counter % 5;

        switch (moduloCounter)
        {

          case 0:
            calc.pointA.transform.position = hit.point;
            break;
          case 1:
            calc.pointB.transform.position = hit.point;
            break;
          case 2:
            calc.pointC.transform.position = hit.point;
            break;
          case 3:
            calc.MakeTriangle();
            targetPivot.position = calc.center.transform.position;
            targetPivot.rotation = calc.center.transform.rotation;
            break;
          case 4:
            //Reset everything
            targetPivot.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 1);
            targetPivot.position = new Vector3(3.5f,0,3);
            calc.center.transform.position = Vector3.left;
            calc.pointA.transform.position = Vector3.left;
            calc.pointB.transform.position = Vector3.left;
            calc.pointC.transform.position = Vector3.left;

            break;
        }

      }
    }
  }
}
