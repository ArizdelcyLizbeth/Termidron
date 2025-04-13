using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Script que maneja la pantalla principal del menú del juego utilizando el sistema de UIElements de Unity.
/// Permite al jugador iniciar el juego, ver las instrucciones, consultar los créditos o salir del juego.
/// </summary>
public class Display0 : MonoBehaviour
{
    public GameManager game;
    public UIManager uiManager;
    private UIDocument display;
    private Button startGame;
    private Button instructions;
    private Button credits;
    private Button exit;

    /// <summary>
    /// Método llamado cuando el objeto se habilita en la escena. 
    /// Registra las interacciones de los botones con sus respectivos eventos de clic.
    /// </summary>
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

    /// <summary>
    /// Método llamado cuando el jugador hace clic en el botón "Play".
    /// Inicia el juego llamando al método StartGame() del GameManager.
    /// </summary>
    /// <param name="evt">El evento de clic que ha activado este método.</param>
    private void StartGame(ClickEvent evt)
    {
        game.StartGame();
    }

    /// <summary>
    /// Método llamado cuando el jugador hace clic en el botón "Instructions".
    /// Desactiva la pantalla actual y activa la pantalla de instrucciones.
    /// </summary>
    /// <param name="evt">El evento de clic que ha activado este método.</param>
    private void ShowInstructions(ClickEvent evt)
    {
        uiManager.DeactivateUI(0);
        uiManager.ActivateUI(3);
    }

    /// <summary>
    /// Método llamado cuando el jugador hace clic en el botón "Credits".
    /// Desactiva la pantalla actual y activa la pantalla de créditos.
    /// </summary>
    /// <param name="evt">El evento de clic que ha activado este método.</param>
    private void ShowCredits(ClickEvent evt)
    {
        uiManager.DeactivateUI(0);
        uiManager.ActivateUI(4);
    }

    /// <summary>
    /// Método llamado cuando el jugador hace clic en el botón "Exit".
    /// Cierra la aplicación.
    /// </summary>
    /// <param name="evt">El evento de clic que ha activado este método.</param>
    private void ExitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}