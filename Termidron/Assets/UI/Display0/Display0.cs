using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display0 : MonoBehaviour
{
    public GameManager game;
    public UIManager uiManager;
    private UIDocument display;
    private Button startGame;
    private Button instructions;
    private Button credits;
    private Button exit;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        startGame = root.Q<Button>("Play");
        instructions = root.Q<Button>("Instructions");
        credits = root.Q<Button>("Credits");
        exit = root.Q<Button>("Exit");
        startGame.RegisterCallback<ClickEvent>(StartGame);
        instructions.RegisterCallback<ClickEvent>(ShowInstructions);
        credits.RegisterCallback<ClickEvent>(ShowCredits);
        exit.RegisterCallback<ClickEvent>(ExitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        game.StartGame();
    }

    private void ShowInstructions(ClickEvent evt)
    {
        uiManager.DeactivateUI(0);
        uiManager.ActivateUI(3);
    }

    private void ShowCredits(ClickEvent evt)
    {
        uiManager.DeactivateUI(0);
        uiManager.ActivateUI(4);
    }

    private void ExitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}