using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingSubscriber : MonoBehaviour
{
    
    void Start()
    {
        HandTracking handTracking = GameObject.Find("HandTrackingController").GetComponent<HandTracking>();
        handTracking.OnHandIsClose += HandTracking_OnHandIsClose;
    }

    private void HandTracking_OnHandIsClose(object sender, HandTracking.OnHandIsCloseEventArgs e)
    {
        Debug.Log("The distance is: " + e.dist);
        //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        HandTracking handTracking = GameObject.Find("HandTrackingController").GetComponent<HandTracking>();
        handTracking.OnHandIsClose -= HandTracking_OnHandIsClose;
    }
}
