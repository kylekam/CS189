using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using TMPro;

public class TutorialItem : ScaleTween
{
    public enum ContinueType { OkButton, Hand, None }
    public enum ComponentAction { None, flipSwitch, turnLeverOut, turnLeverIn }

    [Header("Visual Components")]
    [SerializeField] private TextMeshPro bodyTextMesh;
    [SerializeField] private TextMeshPro stepTextMesh;
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameObject handSymbol;
    public ContinueType continueType;

    [Header("Display Location")]
    [SerializeField] Transform displayParent;

    [Header("Animation")]
    public GameObject component;
    [SerializeField] private ComponentAction componentAction;

    // *** ITutorialItem Implementation ***
    [SerializeField] private int m_itemOrder;
    public int ItemOrder
    {
        get => m_itemOrder;
    }
    // *** End ITutorialItem ***
    public BoolVariable CloseCondition;

    public static List<TutorialItem> GetTutorialItems()
    {
        return Resources.FindObjectsOfTypeAll<TutorialItem>()
            .Where(item => item.gameObject.scene.IsValid())
            .OrderBy(item => item.ItemOrder)
            .ToList();
    }

    public void OnValidate()
    {
        if (stepTextMesh != null)
        {
            int totalItems = GetTutorialItems().Count;
            string stepStr = $"{m_itemOrder.ToString()} / {totalItems.ToString()}";
            stepTextMesh.text = stepStr;
        }

        okButton?.SetActive(false);
        handSymbol?.SetActive(false);
        if (continueType == ContinueType.OkButton) { okButton?.SetActive(true); }
        else if (continueType == ContinueType.Hand) { handSymbol?.SetActive(true); }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Open()
    {
        if (displayParent == null)
        { 
            Debug.Log($"{gameObject.name} has no displayParent");    
        }
        else
        { 
            this.transform.SetPositionAndRotation(displayParent.transform.position, displayParent.parent.rotation);
            this.transform.parent = displayParent;
            this.transform.localPosition = Vector3.zero;
            if (component != null)
            {
                if (component.GetComponent<Renderer>() != null)
                {
                    component.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                } else
                {
                    for (int i = 0; i < component.transform.childCount; i++)
                    {
                        component.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    }
                }
                if (componentAction == ComponentAction.flipSwitch)
                {
                    component.transform.Rotate(0, 0, 180);
                } else if (componentAction == ComponentAction.turnLeverOut)
                {
                    StartCoroutine(rotateLever(90.0f));
                } else if (componentAction == ComponentAction.turnLeverIn)
                {
                    StartCoroutine(rotateLever(-90.0f));
                }
            }
        }
        gameObject.SetActive(true);
        base.TweenIn();
    }

    private IEnumerator rotateLever(float degrees)
    {
        Quaternion target = component.transform.rotation * Quaternion.Euler(0, 0, degrees);
        Quaternion start = component.transform.rotation;
        float duration = 1.0f;
        float timeCount = 0.0f;
        while (timeCount < duration)
        {
            component.transform.rotation = Quaternion.Slerp(start, target, timeCount / duration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        component.transform.rotation = target;
    }

    public virtual void Close()
    {
        base.TweenOut(delegate ()
        {
            gameObject.SetActive(false);
        });
        if (component != null)
        {
            if (component.GetComponent<Renderer>() != null)
            {
                component.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            } else
            {
                for (int i = 0; i < component.transform.childCount; i++)
                {
                    component.transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f, 1f);
                }
            }

        }
    }

    public void OnTutorialEnter()
    {
        Debug.Log($"[Tutorial Item {ItemOrder}]");

    }

    public void OnTutorialExit()
    {

    }

    public void enableOkButton()
    {
        okButton?.SetActive(true);
    }
}
