using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class MenuButtonConfig : MonoBehaviour
{
    private bool instructionsRunning = false;
    public GameObject button;
    private GameObject instructionControl;
    // Start is called before the first frame update
    void Start()
    {
        instructionControl = GameObject.Find("InstructionControl");
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(instructionControl.GetComponent<TutorialMainActivity>().StartActivity);
        button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(toggleButton);
        instructionsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleButton()
    {
        Debug.Log("toggle button");
        if (instructionsRunning == false)
        {
            button.GetComponent<ButtonConfigHelper>().MainLabelText = "Next Instruction";
            button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(instructionControl.GetComponent<TutorialMainActivity>().StartActivity);
            button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(toggleButton);
            button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(TutorialMainActivity.logHandMenuButtonContinue);
            button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(instructionControl.GetComponent<TutorialMainActivity>().NextItem);
            button.GetComponent<ButtonConfigHelper>().SetQuadIconByName("IconHide"); //icon names at Packages/Mixed Reality Toolkit Foundation/SDK/Standard Assets/Textures
            instructionsRunning = true;
        } else
        {
            button.GetComponent<ButtonConfigHelper>().MainLabelText = "Start Instructions";
            button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(TutorialMainActivity.logHandMenuButtonContinue);
            button.GetComponent<ButtonConfigHelper>().OnClick.RemoveListener(instructionControl.GetComponent<TutorialMainActivity>().NextItem);
            button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(instructionControl.GetComponent<TutorialMainActivity>().StartActivity);
            button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(toggleButton);
            button.GetComponent<ButtonConfigHelper>().SetQuadIconByName("IconDone");
            instructionsRunning = false;
        }
    }
}
