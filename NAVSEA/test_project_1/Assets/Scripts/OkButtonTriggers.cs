using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButtonTriggers : MonoBehaviour
{
    private bool isTouched;
    private bool isClose;
    public static TutorialItem currentInstruction;

    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouched && isClose)
        {
            currentInstruction.enableOkButton();
        }
    }
}
