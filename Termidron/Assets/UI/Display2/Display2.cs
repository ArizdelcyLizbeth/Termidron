using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display2 : MonoBehaviour
{
    public GameManager game;
    private UIDocument display;
    private Label title;
    private Label subtitle;
    private Button goToTheStart;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        title = root.Q<Label>("Title");
        subtitle = root.Q<Label>("Subtitle");
        goToTheStart = root.Q<Button>("Return");
        goToTheStart.RegisterCallback<ClickEvent>(GoToTheStart);
    }

    public void UpdateTitleAndSubtitle(string titulo, string subtitulo)
    {
        this.title.text = titulo;
        this.subtitle.text = subtitulo;
    }

    private void GoToTheStart(ClickEvent evt)
    {
        game.InitializeGame();
    }
}