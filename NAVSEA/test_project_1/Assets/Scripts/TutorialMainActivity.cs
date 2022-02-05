using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class TutorialMainActivity : MonoBehaviour
{
    private static List<TutorialItem> tutorialItems;
    private static bool isRunning = false;
    private static bool hasStarted = false;
    private static int instructionNumber = 0;
    public static TutorialItem currentItem = null;
    public static string logFilePath = null;

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
        createLog();
        if (currentItem == null)
        {
            tutorialItems = TutorialItem.GetTutorialItems();
            for (int i=0; i<instructionNumber; i++)
            {
                tutorialItems.Remove(tutorialItems[0]);
            }
            currentItem = tutorialItems[0];
            if (hasStarted)
            {
                currentItem.animationEnabled = false;
            } else
            {
                currentItem.animationEnabled = true;
            }
            currentItem.Open();
            currentItem.OnTutorialEnter();
        }
        isRunning = true;
        hasStarted = true;
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
            log("Instructions stopped");
            GameObject.Find("InstructionControl").GetComponent<MenuButtonConfig>().toggleButton();
        }
        isRunning = false;
        logFilePath = null;
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
            currentItem.playAudio = true;
            currentItem.OnTutorialEnter();
            instructionNumber++;
        } else
        {
            currentItem = null;
            GameObject.Find("InstructionControl").GetComponent<MenuButtonConfig>().toggleButton();
            isRunning = false;
            logFilePath = null;
            instructionNumber = 0;
            hasStarted = false;
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

    public void createLog()
    {
        string logDirectory = null;
        //editor file path: Assets/Logs
        //hololens file path: User Folders/LocalAppData/NAVSEA/LocalState/Logs
#if UNITY_EDITOR
        logDirectory = Path.Combine(Application.dataPath, "Logs");
#elif WINDOWS_UWP
        logDirectory = Path.Combine(Application.persistentDataPath, "Logs");
#endif
        Debug.Log(logDirectory);
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
            Debug.Log("log directory created: " + logDirectory);
        }
        string dateTime = System.DateTime.Now.ToString("MMddyyyyhhmmss");
        string formattedDateTime = System.DateTime.Now.ToString("MM/dd/yyyy, hh:mm:ss");
        logFilePath = Path.Combine(logDirectory, "NAVSEALog" + dateTime + ".txt");
        File.AppendAllText(logFilePath, "Procedure started at " + formattedDateTime + "\n");
        //File.Create(logFilePath).Dispose();
        Debug.Log("log file created: " + logFilePath);
        /*
        var w = File.CreateText(logFilePath);
        w.WriteLine("Procedure started at " + formattedDateTime);
        w.Close();*/
    }

    public static void log(string line)
    {
        File.AppendAllText(logFilePath, line + "\n");
    }

    public void logInstructionButtonContinue()
    {
        Debug.Log("instruction button log");
        string time = System.DateTime.Now.ToString("hh:mm:ss");
        File.AppendAllText(logFilePath, time + ": Clicked button in instruction\n");
    }

    public static void logHandMenuButtonContinue()
    {
        string time = System.DateTime.Now.ToString("hh:mm:ss");
        Debug.Log("hand menu button log");
        File.AppendAllText(logFilePath, time + ": Clicked next step button in hand menu\n");
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
