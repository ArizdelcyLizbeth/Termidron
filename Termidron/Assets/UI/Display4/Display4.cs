using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display4 : MonoBehaviour
{
    public UIManager uiManager;
    private UIDocument display;
    private Button back;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        back = root.Q<Button>("Back");
        back.RegisterCallback<ClickEvent>(ShowMainDisplay);
    }

    private void ShowMainDisplay(ClickEvent evt)
    {
        uiManager.DeactivateUI(4);
        uiManager.ActivateUI(0);
    }
}
