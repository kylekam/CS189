using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TutorialMainActivity : MonoBehaviour
{
    private static List<TutorialItem> tutorialItems;
    private static bool isRunning = false;
    public static TutorialItem currentItem = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"TutorialMainActivity Start");
        currentItem = null;
    }

    // Update is called once per frame
    void Update() {}

    public void StartActivity()
    {
        Debug.Log("start activity");
        if (currentItem == null)
        {
            tutorialItems = TutorialItem.GetTutorialItems();
            currentItem = tutorialItems[0];
            currentItem.Open();
            currentItem.OnTutorialEnter();
        }
        isRunning = true;
    }

    public void StopActivity()
    {
        tutorialItems = TutorialItem.GetTutorialItems();
        foreach(var tutorialItem in tutorialItems)
        {
            tutorialItem.Close();
            tutorialItem.OnTutorialExit();
            currentItem = null;
        }
        if (isRunning)
        {
            GameObject.Find("InstructionControl").GetComponent<MenuButtonConfig>().toggleButton();
        }
        isRunning = false;
    }

    public void NextItem()
    {
        Debug.Log("next item");
        // Close current step
        currentItem.Close();
        currentItem.OnTutorialExit();
        tutorialItems.Remove(currentItem);

        // Start next step
        if (tutorialItems.Count > 0)
        {
            currentItem = tutorialItems[0];
            if (currentItem.CloseCondition != null)
            {
                currentItem.CloseCondition.OnValueChanged += CloseConditionHandler;
            }
            currentItem.Open();
            currentItem.OnTutorialEnter();
        } else
        {
            currentItem = null;
            GameObject.Find("InstructionControl").GetComponent<MenuButtonConfig>().toggleButton();
            isRunning = false;
        }
    }

    public void CloseConditionHandler(bool newValue)
    {
        if (newValue == true)
        { 
            currentItem.CloseCondition.OnValueChanged -= CloseConditionHandler;
            NextItem();
        }
    }

    public void enableCurrentOkButton()
    {
        currentItem.enableOkButton();
    }

    // public void OnItemOpen()
    // {
    //     AudioSource audio = currentItem.gameObject.GetComponent<AudioSource>();
    //     audio.clip = currentItem.dialogue.audioClip;
    //     audio.Play();
    // }

    // public void OnItemClose()
    // {
    //     AudioSource audio = currentItem.gameObject.GetComponent<AudioSource>();
    //     audio.Stop();
    // }
}
