using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display2 : MonoBehaviour
{
    public GameManager game;
    private UIDocument display;
    private Label text;
    private Button goToTheStart;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        text = root.Q<Label>("MainLabel");
        goToTheStart = root.Q<Button>("Return");
        goToTheStart.RegisterCallback<ClickEvent>(GoToTheStart);
    }

    private void GoToTheStart(ClickEvent evt)
    {
        game.InitializeGame();
    }
}