using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiElements;

    public void UpdateTimer(string time)
    {
        Display1 display1 = uiElements[1].GetComponent<Display1>();
        display1.UpdateTime(time);
    }

    public void UpdateFinal(string title, string subtitle)
    {
        Display2 display2 = uiElements[2].GetComponent<Display2>();
        display2.UpdateTitleAndSubtitle(title, subtitle);
    }

    public void ActivateUI(int index)
    {
        if (index >= 0 && index < uiElements.Length)
        {
            uiElements[index].SetActive(true);
        }
    }

    public void DeactivateUI(int index)
    {
        if (index >= 0 && index < uiElements.Length)
        {
            uiElements[index].SetActive(false);
        }
    }

    public void DeactivateAllUI()
    {
        foreach (var element in uiElements)
        {
            element.SetActive(false);
        }
    }

    public void ActivateAllUI()
    {
        foreach (var element in uiElements)
        {
            element.SetActive(true);
        }
    }
}