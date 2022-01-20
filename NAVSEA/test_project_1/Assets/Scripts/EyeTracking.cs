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
            Outline outline;

            if (!go.CompareTag("LightSwitch"))
            {
                outline = go.GetComponent<Outline>();
                outline.OutlineWidth = 2;
                outline.enabled = true;

                print(go.name + " Outline enabled: " + outline.enabled + " IsActiveAndEnabled: " + outline.isActiveAndEnabled);
            }

            else
            {
                print(go.name);
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    outline = go.transform.GetChild(i).GetComponent<Outline>();
                    outline.OutlineWidth = 2;
                    outline.enabled = true;
                }
            }

            if (lastGameObject != null && lastGameObject != go)
            {
                if (!lastGameObject.CompareTag("LightSwitch"))
                {
                    outline = lastGameObject.GetComponent<Outline>();
                    outline.enabled = false;
                }
                else
                {
                    for (int i = 0; i < lastGameObject.transform.childCount; i++)
                    {
                        outline = lastGameObject.transform.GetChild(i).GetComponent<Outline>();
                        outline.enabled = false;
                    }
                }
            }

            lastGameObject = go;
        }
        else
        {
            if (lastGameObject != null) 
            {
                Outline outline;

                if (lastGameObject.CompareTag("LightSwich"))
                {
                    for (int i = 0; i < lastGameObject.transform.childCount; i++)
                    {
                        outline = lastGameObject.transform.GetChild(i).GetComponent<Outline>();
                        outline.enabled = false;
                    }
                }
                else
                {
                    outline = lastGameObject.GetComponent<Outline>();
                    outline.enabled = false;
                }
            }
        }
    }
}
