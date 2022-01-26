using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjEvent : Interactable { }

public class CubeInteraction : MonoBehaviour
{

    public GameObjEvent OnMyCubeTouched;

    // Start is called before the first frame update
    void Start()
    {
        //var onTouchReceiver = GameObjEvent.AddReceiver<InteractableOnTouchReceiver>();
        //OnMyCubeTouched.OnClick.AddListener(() => Debug.Log("Object has been clicked"));
        //OnMyCubeTouched.OnTouchCompleted.AddListener(() => Debug.Log(" touched"));
        OnMyCubeTouched.OnClick.AddListener(() => Debug.Log("Interactable clicked"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyEvent(GameObject go)
    {
        Debug.Log(go.name);
    }

    public static void AddOnClick(Interactable interactable)
    {
        interactable.OnClick.AddListener(() => Debug.Log("Interactable clicked"));
    }
}
