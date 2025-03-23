using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display1 : MonoBehaviour
{
    private UIDocument display;
    private Label timer;
    private Label keys;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        timer = root.Q<Label>("Timer");
        keys = root.Q<Label>("Keys");
    }

    public void UpdateTime(string time)
    {
        timer.text = time;
    }

    public void UpdateKeysCounter(int number) {
        keys.text = number.ToString();
    }
}