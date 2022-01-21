using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class EyeTracking : MonoBehaviour
{
    private GameObject lastGameObject;
    private Color lastColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject gameObject = CoreServices.InputSystem.EyeGazeProvider.HitInfo.collider.gameObject;
        if (gameObject != null)
        {
            Outline outline;

            if (!gameObject.CompareTag("LightSwitch"))
            {
                outline = gameObject.GetComponent<Outline>();
                outline.OutlineWidth = 2;
                outline.enabled = true;

                print(gameObject.name + " Outline enabled: " + outline.enabled + " IsActiveAndEnabled: " + outline.isActiveAndEnabled);
            }
            else
            {
                print(gameObject.name);
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    outline = gameObject.transform.GetChild(i).GetComponent<Outline>();
                    outline.OutlineWidth = 2;
                    outline.enabled = true;
                }
            }

            if (lastGameObject != null && lastGameObject != gameObject)
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

            lastGameObject = gameObject;
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


        // HEAD-GAZE CODE

        /*RaycastHit hitInfo;
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
        }*/
    }
}
