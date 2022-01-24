using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Gesture : MonoBehaviour
{
    public static GameObject component;
    private GestureRecognizer recognizer;

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Debug.Log("Tap experienced");
    }

    // Start is called before the first frame update
    void Start()
    {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += GestureRecognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
