using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Script que maneja la pantalla de presentación o transición en el juego, mostrando un título,
/// un subtítulo y un botón para regresar al inicio del juego.
/// </summary>
public class Display2 : MonoBehaviour
{
    public GameManager game;
    private UIDocument display;
    private Label title;
    private Label subtitle;
    private Button goToTheStart;

    /// <summary>
    /// Método llamado cuando el objeto se habilita en la escena.
    /// Inicializa los elementos visuales de la UI y registra el callback del botón.
    /// </summary>
    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        title = root.Q<Label>("Title");
        subtitle = root.Q<Label>("Subtitle");
        goToTheStart = root.Q<Button>("Return");
        goToTheStart.RegisterCallback<ClickEvent>(GoToTheStart);
    }

    /// <summary>
    /// Actualiza el texto del título y del subtítulo en la UI.
    /// </summary>
    /// <param name="titulo">El texto del título que se mostrará.</param>
    /// <param name="subtitulo">El texto del subtítulo que se mostrará.</param>
    public void UpdateTitleAndSubtitle(string titulo, string subtitulo)
    {
        this.title.text = titulo;
        this.subtitle.text = subtitulo;
    }

    /// <summary>
    /// Método que se ejecuta cuando se hace clic en el botón para regresar al inicio.
    /// Llama al método de GameManager para reiniciar el juego o regresar al estado inicial.
    /// </summary>
    /// <param name="evt">El evento de clic que activa el callback.</param>
    private void GoToTheStart(ClickEvent evt)
    {
        game.InitializeGame();
    }
}