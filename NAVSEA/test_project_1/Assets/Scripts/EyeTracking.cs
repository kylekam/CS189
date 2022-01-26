using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class EyeTracking : MonoBehaviour
{
    private GameObject lastGameObject;
    private Color lastColor;
    //private bool objectFound = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject go = CoreServices.InputSystem.EyeGazeProvider.HitInfo.collider.gameObject;

        if(lastGameObject == null)
        {
            if (go.GetComponent<Renderer>() != null)
            {

                var renderer = go.GetComponent<Renderer>();
                lastGameObject = go;
                lastColor = renderer.material.color;
                renderer.material.SetColor("_Color", Color.green);
            }
            else
            {
                /*for (int i = 0; i < go.transform.childCount; i++)
                {
                    go.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.green); //green
                }*/
                lastGameObject = go;
                //lastColor = Color.white;
                print("switch 1");
            }

        }
        else if (go != lastGameObject)
        {
            if (lastGameObject.GetComponent<Renderer>() != null)
            {
                var renderer = lastGameObject.GetComponent<Renderer>();
                renderer.material.SetColor("_Color", lastColor);
            }
            else
            {
                /*for (int i = 0; i < lastGameObject.transform.childCount; i++)
                {
                    lastGameObject.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", lastColor); //white
                }*/
                print("switch 2");
            }

            if (go.GetComponent<Renderer>() != null)
            {
                var renderer = go.GetComponent<Renderer>();
                lastGameObject = go;
                lastColor = renderer.material.color;
                renderer.material.SetColor("_Color", Color.green);
            }
            else
            {
                /*for (int i = 0; i < go.transform.childCount; i++)
                {
                    go.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.green); //green
                }*/
                lastGameObject = go;
                //lastColor = Color.white;
                print("switch 3");
            }

        }
        else
        {
            if (lastGameObject != null)
            {
                 if (lastGameObject.GetComponent<Renderer>() != null)
                {
                    var renderer = lastGameObject.GetComponent<Renderer>();
                    renderer.material.SetColor("_Color", lastColor);
                }
                else
                {
                   /* for (int i = 0; i < lastGameObject.transform.childCount; i++)
                    {
                        lastGameObject.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", lastColor);
                    }*/
                    print("switch 4");
                }

                lastGameObject = null;
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
