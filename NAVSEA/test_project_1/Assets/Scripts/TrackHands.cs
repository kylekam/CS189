using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using System;

public class TrackHands : MonoBehaviour
{
    private float howClose = 0.3f;

    public static GameObject component;
    public static TutorialItem currentInstruction;
    public static string componentName;
    public bool hasBeenTriggered = false; // So that it only detects it once.

    MixedRealityPose poseRight;
    MixedRealityPose poseLeft;

    void Start()
    {
        component = null;
        currentInstruction = null;
    }

    void Update()
    {
        if (!hasBeenTriggered && component != null && Equals(this.name, component.name))
        {
            var pos = GameObject.Find(componentName).transform.position;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out poseRight) || HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out poseLeft))
            {
                float distRight = Vector3.Distance(pos, poseRight.Position);
                float distLeft = Vector3.Distance(pos, poseLeft.Position);
                //Debug.Log("Location of object is: " + pos);
                //Debug.Log("Distance between hand and object is: " + dist);
                if (distRight < howClose || distLeft < howClose)
                {
                    currentInstruction.isClose = true;
                    hasBeenTriggered = true;
                    Debug.Log("hands are close to: " + componentName);
                    currentInstruction.enableOkButton(); // Checks if all conditions are true
                }
            }
        }
    }
    public static void checkComponent(TutorialItem currentItem, GameObject itemComponent)
    {
        component = itemComponent;
        currentInstruction = currentItem;
        if (component != null)
        {
            componentName = component.transform.name;
        } else
        {
            componentName = "";
        }
    }

}
