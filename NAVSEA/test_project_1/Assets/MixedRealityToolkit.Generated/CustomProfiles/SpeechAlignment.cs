using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechAlignment : MonoBehaviour, IMixedRealitySpeechHandler
{
    public TriangleCalculator calc;
    public Transform targetPivot;

    private int counter = -1;
    MixedRealityPose poseRight;

    // Start is called before the first frame update
    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        // Get right pointer finger location
        // Spawn orb

        switch (eventData.Command.Keyword.ToLower())
        {
            case "align":
                Debug.Log("Hello World");
                if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out poseRight))
                {
                    counter++;
                    int moduloCounter = counter % 5;

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
                            break;
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
                break;

            default:
                Debug.Log($"Unknown option { eventData.Command.Keyword}");
                break;

        }
    }
}
