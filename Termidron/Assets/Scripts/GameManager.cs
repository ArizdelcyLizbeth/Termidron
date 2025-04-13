using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador principal del juego. Gestiona el ciclo de vida del juego (inicio, juego, fin),
/// administra temporizadores, sonidos, drones, llaves y la interfaz de usuario.
/// </summary>
public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject[] keys;
    public GameObject mainCamera;
    public GameObject mainCharacter;
    public KeyCollector keyCollector;
    public GameObject[] drones;

    public AudioClip majorSound;
    public AudioClip loseSound;
    private AudioSource audioSource;

    private enum GameState { Start, Gaming, Finish }
    private GameState currentState;
    private float countdownTime;

    /// <summary>
    /// Inicializa el estado del juego y agrega un componente de audio.
    /// </summary>
    void Start()
    {
        SetState(GameState.Start);
        audioSource = gameObject.AddComponent<AudioSource>();  
    }

    /// <summary>
    /// Devuelve true si el juego está en curso.
    /// </summary>
    public bool IsGameInProgress()
    {
        return currentState == GameState.Gaming;
    }

    /// <summary>
    /// Restaura la posición inicial del personaje y la cámara, y reinicia las llaves.
    /// </summary>
    public void InitializeGame() 
    {
        mainCharacter.transform.position = new Vector3(0f, 0f, 0f);
        mainCharacter.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        mainCamera.transform.position = new Vector3(0f, 3f, -5f);
        mainCamera.transform.rotation = Quaternion.Euler(3.5f, 0f, 0f);
        SetState(GameState.Start);
        foreach (GameObject key in keys)
        {
            key.SetActive(true);
        }
        keyCollector.ResetKeysCollected();
    }

    /// <summary>
    /// Cambia el estado actual del juego y ejecuta la lógica asociada.
    /// </summary>
    private void SetState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.Start:
                StartGameLogic();
                uiManager.ActivateUI(0);
                break;
            case GameState.Gaming:
                UpdateGameLogic();
                uiManager.ActivateUI(1);
                break;
            case GameState.Finish:
                FinishGameLogic();
                uiManager.ActivateUI(2);
                break;
        }
    }

    /// <summary>
    /// Inicia el juego si está en estado de inicio.
    /// </summary>
    public void StartGame()
    {
        if (currentState == GameState.Start)
        {
            uiManager.DeactivateUI(0);
            SetState(GameState.Gaming);
            StartCoroutine(CountdownCoroutine());
            PlayMajorSound();  
        }
    }

    /// <summary>
    /// Finaliza la partida actual si se encuentra en progreso.
    /// Cambia el estado del juego a "Finish", actualiza la interfaz de usuario con un mensaje personalizado,
    /// y reproduce un sonido de derrota.
    /// </summary>
    /// <param name="title">Título principal que se mostrará al finalizar el juego.</param>
    /// <param name="subtitle">Subtítulo o mensaje adicional con detalles del final del juego.</param>
    public void FinishGame(string title, string subtitle)
    {
        if (currentState == GameState.Gaming)
        {
            SetState(GameState.Finish);
            uiManager.UpdateFinal(title, subtitle);
            PlayLoseSound();  
        }
    }

    /// <summary>
    /// Reinicia el juego si está en estado finalizado.
    /// </summary>
    public void RestartGame()
    {
        if (currentState == GameState.Finish)
        {
            SetState(GameState.Start);
        }
    }

    /// <summary>
    /// Corrutina que actualiza el temporizador cada segundo y finaliza el juego si se agota el tiempo.
    /// </summary>
    private IEnumerator CountdownCoroutine()
    {
        while (countdownTime > 0 && IsGameInProgress())
        {
            int minutes = Mathf.FloorToInt(countdownTime / 60);
            int seconds = Mathf.FloorToInt(countdownTime % 60);
            uiManager.UpdateTimer($"{minutes:00}:{seconds:00}");
            countdownTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
        uiManager.UpdateTimer("00:00");
        FinishGame("DEMASIADO TARDE", "La salida estuvo tan cerca  pero el tiempo no tuvo piedad. Inténtalo otra vez.");
    }

    /// <summary>
    /// Devuelve true si aún queda tiempo en el cronómetro.
    /// </summary>
    public bool IsTimeRemaining()
    {
        return countdownTime > 0;
    }

    /// <summary>
    /// Lógica de preparación cuando el juego entra en estado Start.
    /// </summary>
    private void StartGameLogic()
    {
        uiManager.DeactivateAllUI();
        countdownTime = 300f;
    }

    /// <summary>
    /// Lógica activa durante el juego (habilita drones).
    /// </summary>
    private void UpdateGameLogic()
    {
        foreach (GameObject drone in drones)
        {
            drone.GetComponent<Drone>().EnableGaming();
        }
    }

    /// <summary>
    /// Lógica cuando el juego ha terminado (reinicia drones y UI).
    /// </summary>
    private void FinishGameLogic()
    {
        foreach (GameObject drone in drones)
        {
            drone.GetComponent<Drone>().ResetToInitialPosition();
        }
        uiManager.DeactivateUI(1);
    }

    /// <summary>
    /// Reproduce el audio del inicio del juego.
    /// </summary>
    private void PlayMajorSound()
    {
        if (audioSource.isPlaying) return;  
        audioSource.clip = majorSound;
        audioSource.loop = true; 
        audioSource.Play();
    }

    /// <summary>
    /// Reproduce el audio de finalización del juego.
    /// </summary>
    private void PlayLoseSound()
    {
        audioSource.clip = loseSound;
        audioSource.loop = false;  
        audioSource.Play();
    }
}