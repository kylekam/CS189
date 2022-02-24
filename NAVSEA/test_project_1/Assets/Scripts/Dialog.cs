using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject text;
    public GameObject dialog;
    private int count = 0;
    private string message1 = "To align the virtual board with the physical board, tap the “Enable Alignment” button at the bottom of the hand menu.";
    private string message2 = "Then, tap any three corners of the physical board in a counter clockwise direction using your right hand.";
    private string message3 = "After each tap, pinch the fingers of your left hand. After completing the taps, pinch your fingers again and the virtual board should snap into place.";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleText()
    {
        if (count == 0)
        {
            text.GetComponent<Text>().text = message1;
            count++;
        }
        else if (count == 1)
        {
            text.GetComponent<Text>().text = message2;
            count++;
        }
        else if (count == 2)
        {
            text.GetComponent<Text>().text = message3;
            count++;
        }
        else
        {
            dialog.SetActive(false);
        }
    }
}
