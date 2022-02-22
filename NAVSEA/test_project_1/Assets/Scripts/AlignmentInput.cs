using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentInput : MonoBehaviour, IMixedRealitySpeechHandler
{
    // Used for calculating triangle center
    public TriangleCalculator calc;
    public Transform targetPivot;

    private int counter = -1;
    private int moduloCounter;

    // Used for hand tracking
    MixedRealityPose poseRight;
    MixedRealityPose poseLeftThumb;
    MixedRealityPose poseLeftPointer;

    private bool alightnmentEnabled;
    private bool pinchCompleted; // Used so that the pinch is only registered once

    public GameObject button;

    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(enableAlignment);
        alightnmentEnabled = false;
        pinchCompleted = false;
    }


    void Update()
    {
        if (alightnmentEnabled)
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out poseLeftThumb)
                && HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out poseLeftPointer)
                && !pinchCompleted
                && 0.025 > Vector3.Distance(poseLeftThumb.Position, poseLeftPointer.Position))
            {
                tryAlignment();
                pinchCompleted = true;
                Debug.Log("pinch!");
            }

            // Renable pinch after fingers have opened up a bit
            if (0.045 < Vector3.Distance(poseLeftThumb.Position, poseLeftPointer.Position))
            {
                pinchCompleted = false;
            }
        }
    }

    public void enableAlignment()
    {
        alightnmentEnabled = true;
        this.GetComponentInParent<ObjectManipulator>().enabled = false;
        button.GetComponent<ButtonConfigHelper>().MainLabelText = "Disable Alignment";
        button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(enableAlignment);
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(disableAlignment);
        button.GetComponent<ButtonConfigHelper>().SetQuadIconByName("IconClose"); //icon names at Packages/Mixed Reality Toolkit Foundation/SDK/Standard Assets/Textures
    }

    public void disableAlignment()
    {
        alightnmentEnabled = false;
        button.GetComponent<ButtonConfigHelper>().MainLabelText = "Enable Alignment";
        button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(disableAlignment);
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(enableAlignment);
        button.GetComponent<ButtonConfigHelper>().SetQuadIconByName("IconAdjust"); //icon names at Packages/Mixed Reality Toolkit Foundation/SDK/Standard Assets/Textures

        //Make all components invisible
        calc.pointA.GetComponent<MeshRenderer>().enabled = false;
        calc.pointB.GetComponent<MeshRenderer>().enabled = false;
        calc.pointC.GetComponent<MeshRenderer>().enabled = false;
        calc.center.GetComponent<MeshRenderer>().enabled = false;
    }


    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (alightnmentEnabled)
        {
            switch (eventData.Command.Keyword.ToLower())
            {
                case "align":
                    Debug.Log("Word recognized.");
                    tryAlignment();
                    break;
                default:
                    Debug.Log($"Unknown option { eventData.Command.Keyword}");
                    break;
            }
        }
    }


    void tryAlignment()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out poseRight))
        {
            counter++;
            moduloCounter = counter % 5;

            // Spawn orb at location of finger
            switch (moduloCounter)
            {
                case 0:
                    calc.pointA.transform.position = poseRight.Position;
                    break;
                case 1:
                    calc.pointB.transform.position = poseRight.Position;
                    break;
                case 2:
                    calc.pointC.transform.position = poseRight.Position;
                    break;
                case 3:
                    calc.MakeTriangle();
                    targetPivot.position = calc.center.transform.position;
                    targetPivot.rotation = calc.center.transform.rotation;
                    disableAlignment();
                    

                    break;

                // CASE 4 IS NOT CURRENTLY USED. IT IS USED FOR THE ALIGNMENT CYCLE.
                case 4:
                    //Reset everything
                    targetPivot.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 1);
                    targetPivot.position = new Vector3(3.5f, 0, 3);
                    calc.center.transform.position = Vector3.left;
                    calc.pointA.transform.position = Vector3.left;
                    calc.pointB.transform.position = Vector3.left;
                    calc.pointC.transform.position = Vector3.left;
                    break;
            }
        }
    }
}


