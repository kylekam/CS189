using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TutorialMainActivity : MonoBehaviour
{
    private List<TutorialItem> tutorialItems;
    private TutorialItem currentItem = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"TutorialMainActivity Start");
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
    }

    public void NextItem()
    {
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
