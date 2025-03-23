using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display0 : MonoBehaviour
{
    public GameManager game;
    private UIDocument display;
    private Button startGame;
    private Button credits;
    private Button exit;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        startGame = root.Q<Button>("Play");
        credits = root.Q<Button>("Credits");
        exit = root.Q<Button>("Exit");
        startGame.RegisterCallback<ClickEvent>(StartGame);
        credits.RegisterCallback<ClickEvent>(ShowCredits);
        exit.RegisterCallback<ClickEvent>(ExitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        game.StartGame();
    }

    private void ShowCredits(ClickEvent evt)
    { 
        
    }

    private void ExitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}