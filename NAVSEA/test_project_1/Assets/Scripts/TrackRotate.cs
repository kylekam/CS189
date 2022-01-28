using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class TrackRotate : MonoBehaviour
{
    public static GameObject component;
    public static TutorialItem currentInstruction;

    // Start is called before the first frame update
    void Start()
    {
        component = null;
        currentInstruction = null;
        this.gameObject.GetComponent<ObjectManipulator>().OnManipulationStarted.AddListener((x) => {
            Debug.Log("Item rotated: " + this.name);
            if (component != null)
            {
                if (Equals(this.name, component.name))
                {
                    currentInstruction.isRotated = true;
                    currentInstruction.enableOkButton();
                }
            }
            if (TutorialMainActivity.logFilePath != null)
            {
                string time = System.DateTime.Now.ToString("hh:mm:ss");
                TutorialMainActivity.log(time + ": Rotated " + this.name);
            }
        }); 
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
}
