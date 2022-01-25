using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class TrackTouch : MonoBehaviour, IMixedRealityTouchHandler
{
    public static GameObject component;
    public static TutorialItem currentInstruction;
    // Start is called before the first frame update
    void Start()
    {
        component = null;
        currentInstruction = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void checkComponent(TutorialItem currentItem, GameObject itemComponent)
    {
        component = itemComponent;
        currentInstruction = currentItem;
    }

    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {
        if (component != null)
        {
            if (Equals(this.name, component.name))
            {
                currentInstruction.isTouched = true;
                currentInstruction.enableOkButton();
            }
        }
        if (TutorialMainActivity.logFilePath != null)
        {
            string time = System.DateTime.Now.ToString("hh:mm:ss");
            TutorialMainActivity.log(time + ": Touched " + this.name);
        }
    }

    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        //Debug.Log("touch completed: " + this.name);
        if (TutorialMainActivity.logFilePath != null)
        {
            string time = System.DateTime.Now.ToString("hh:mm:ss");
            TutorialMainActivity.log(time + ": Stopped touching " + this.name);
        }
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        //Debug.Log("touch updated: " + this.name);
    }

}
