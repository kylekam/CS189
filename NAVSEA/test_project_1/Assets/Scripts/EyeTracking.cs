using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class EyeTracking : MonoBehaviour
{
    private GazeProvider gazeProvider;
    private GameObject lastGameObject;
    private Color lastColor;

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
            if (lastGameObject == null)
            {
                var renderer = go.GetComponent<Renderer>();

                lastGameObject = go;
                lastColor = renderer.material.color;
                renderer.material.SetColor("_Color", Color.green);
            } else if (go != lastGameObject)
            {
                var renderer = lastGameObject.GetComponent<Renderer>();
                renderer.material.SetColor("_Color", lastColor);

                renderer = go.GetComponent<Renderer>();
                lastGameObject = go;
                lastColor = renderer.material.color;
                renderer.material.SetColor("_Color", Color.green);
            }

            print(hitInfo.point);
            if (go.CompareTag("Board"))
            {
                print("HIT BOARD");
            }
        } else
        {
            if (lastGameObject != null) 
            { 
            var renderer = lastGameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", lastColor);

            lastGameObject = null;
            }
        }
    }
}
