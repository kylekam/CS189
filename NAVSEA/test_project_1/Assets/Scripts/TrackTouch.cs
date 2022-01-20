using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class TrackTouch : MonoBehaviour, IMixedRealityTouchHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {
        /*
        TutorialItem currentItem = GameObject.Find("InstructionControl").GetComponent<TutorialMainActivity>().currentItem;
        if (currentItem != null)
        {
            GameObject component = currentItem.component;
            if (component != null)
            {
                if (this.name == component.name)
                {
                    Debug.Log("instruction item touched " + this.name);
                }
            }
        }
        */
        Debug.Log("touch started: " + this.name);
    }

    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        //Debug.Log("touch completed: " + this.name);
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        //Debug.Log("touch updated: " + this.name);
    }

}
