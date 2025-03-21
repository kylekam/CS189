﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class EyeTracking : MonoBehaviour
{
    private GameObject lastGameObject;
    private Color DEFAULT_COLOR = Color.white;
    private Color HIGHLIGHT_COLOR = Color.green;
    private static bool highlightEnabled = false;
    public static GameObject component = null;
    public static TutorialItem currentInstruction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = CoreServices.InputSystem.EyeGazeProvider.HitInfo.collider?.gameObject;
        if (component != null && go != null)
        {
            //Debug.Log("looked at " + go.name);
            if (Equals(go.name, component.name))
            {
                currentInstruction.isGazed = true;
                currentInstruction.enableOkButton(); // Checks if all conditions are true
                highlight(go);
                highlightEnabled = false;
            }
        }
        if (highlightEnabled)
        {
            highlight(go);
        }

        /* GameObject go = CoreServices.InputSystem.EyeGazeProvider.HitInfo.collider?.gameObject;
         highlight(go);*/
    }

    public static void checkComponent(TutorialItem currentItem, GameObject itemComponent)
    {
        component = itemComponent;
        currentInstruction = currentItem;
        if(component != null)
        {
            highlightEnabled = true;
        }
    }

    public static void disableHighlight()
    {
        highlightEnabled = false;
    }

    void highlight(GameObject go)
    {
        //GameObject go = CoreServices.InputSystem.EyeGazeProvider.HitInfo.collider?.gameObject;

        if (go != null && (go.name == "Model" || go.name == "System")) { return; }

        if (go != null && !go.CompareTag("LightSwitch") && !go.CompareTag("Lever") && go.GetComponent<Renderer>() == null) { return; }

        if (go != null && !go.CompareTag("Board"))
        {
            if (go != lastGameObject)
            {
                if (TutorialMainActivity.logFilePath != null)
                {
                    string time = System.DateTime.Now.ToString("hh:mm:ss");
                    TutorialMainActivity.log(time + ": Looked at " + go.name);
                }
                if (lastGameObject != null)
                {
                    changeColor(lastGameObject, DEFAULT_COLOR);
                }
                lastGameObject = go;
            }

            changeColor(go, HIGHLIGHT_COLOR);
        }

        else if (lastGameObject != null)
        {
            changeColor(lastGameObject, DEFAULT_COLOR);
            lastGameObject = null;
        }
    }

    void changeColor(GameObject gameObject, Color color)
    {
        if (gameObject == null)
        {
            return;
        }

        if (gameObject.CompareTag("LightSwitch") || gameObject.CompareTag("Lever"))
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", color); //green
            }
        } else
        {
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color);
        }

        this.lastGameObject = gameObject;
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
