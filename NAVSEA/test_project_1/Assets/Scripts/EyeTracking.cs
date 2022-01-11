using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class EyeTracking : MonoBehaviour
{
    private GazeProvider gazeProvider;

    // Start is called before the first frame update
    void Start()
    {
        gazeProvider = new GazeProvider();
    }

    // Update is called once per frame
    void Update()
    {
        /*GameObject gazeTarget = gazeProvider.GazeTarget;
        IMixedRealityInputSource gazeInputSource = gazeProvider.GazeInputSource;
        Debug.Log("GazeInputSource: " + gazeInputSource.ToString());*/

        /*Debug.Log("IsEyeTrackingDataValid: " + gazeProvider.IsEyeTrackingDataValid);
        Debug.Log("IsEyeCalibrationValid: " + gazeProvider.IsEyeCalibrationValid);
        Debug.Log("IsEyeTrackingEnabled: " + gazeProvider.IsEyeTrackingEnabled);
        Debug.Log("IsEyeTrackingEnabledAndValid: " + gazeProvider.IsEyeTrackingEnabledAndValid);*/

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo))
        {
            GameObject go = hitInfo.collider.gameObject;
            print(hitInfo.point);
            if (go.CompareTag("Board"))
            {
                print("HIT BOARD");
            }
        }
    }
}
