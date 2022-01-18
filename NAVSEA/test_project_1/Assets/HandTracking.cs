using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using System;

public class HandTracking : MonoBehaviour
{
    [SerializeField]
    private float howClose;

    [SerializeField]
    private GameObject trackedObject;
    private string trackedObjectName;

    MixedRealityPose poseRight;
    MixedRealityPose poseLeft;

    public event EventHandler<OnHandIsCloseEventArgs> OnHandIsClose;
    public class OnHandIsCloseEventArgs : EventArgs
    {
        public float dist;
    }

    void Start()
    {
        trackedObjectName = trackedObject.transform.name;
        Debug.Log(trackedObjectName);
    }

    void Update()
    {
        // If hand is close to the cube, then change the color of the cube to red
        var pos = GameObject.Find(trackedObjectName).transform.position;
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out poseRight) || HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out poseLeft))
        {
            float distRight = Vector3.Distance(pos, poseRight.Position);
            float distLeft = Vector3.Distance(pos, poseLeft.Position);
            //Debug.Log("Location of cube is: " + pos);
            //Debug.Log("Distance between hand and cube is: " + dist);
            if (OnHandIsClose != null && (distRight < howClose || distLeft < howClose))
            {
                OnHandIsClose.Invoke(this, new OnHandIsCloseEventArgs {dist = Mathf.Min(distRight,distLeft) }) ;
            }
        }
    }
}
