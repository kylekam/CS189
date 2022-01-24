using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class TapGestureEvent : MonoBehaviour
{
    public static GameObject component;
    private GestureRecognizer recognizer;

    private void Recognizer_Tapped(TappedEventArgs args)
    {
        Debug.Log("Tap experienced");
    }

    // Start is called before the first frame update
    void Start()
    {
        recognizer = new GestureRecognizer();
        recognizer.Tapped += Recognizer_Tapped;
        recognizer.SetRecognizableGestures(GestureSettings.Tap); 
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
