using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SystemMovementToggle : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(disableMovement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableMovement()
    {
        this.GetComponent<ObjectManipulator>().enabled = false;
        button.GetComponent<ButtonConfigHelper>().MainLabelText = "Enable Movement";
        button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(disableMovement);
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(enableMovement);
        button.GetComponent<ButtonConfigHelper>().SetQuadIconByName("IconHandRay"); //icon names at Packages/Mixed Reality Toolkit Foundation/SDK/Standard Assets/Textures
    }

    public void enableMovement()
    {
        this.GetComponent<ObjectManipulator>().enabled = true;
        button.GetComponent<ButtonConfigHelper>().MainLabelText = "Disable Movement";
        button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(enableMovement);
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(disableMovement);
        button.GetComponent<ButtonConfigHelper>().SetQuadIconByName("IconHandMesh"); //icon names at Packages/Mixed Reality Toolkit Foundation/SDK/Standard Assets/Textures
    }
}
