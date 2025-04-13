using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        SetState(GameState.Start);
        audioSource = gameObject.AddComponent<AudioSource>();  
    }

    public bool IsGameInProgress()
    {
        return currentState == GameState.Gaming;
    }

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

    public void FinishGame(string title, string subtitle)
    {
        if (currentState == GameState.Gaming)
        {
            SetState(GameState.Finish);
            uiManager.UpdateFinal(title, subtitle);
            PlayLoseSound();  
        }
    }

    public void RestartGame()
    {
        if (currentState == GameState.Finish)
        {
            SetState(GameState.Start);
        }
    }

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

    public bool IsTimeRemaining()
    {
        return countdownTime > 0;
    }

    private void StartGameLogic()
    {
        uiManager.DeactivateAllUI();
        countdownTime = 300f;
    }

    private void UpdateGameLogic()
    {
        foreach (GameObject drone in drones)
        {
            drone.GetComponent<Drone>().EnableGaming();
        }
    }

    private void FinishGameLogic()
    {
        foreach (GameObject drone in drones)
        {
            drone.GetComponent<Drone>().ResetToInitialPosition();
        }
        uiManager.DeactivateUI(1);
    }

    private void PlayMajorSound()
    {
        if (audioSource.isPlaying) return;  
        audioSource.clip = majorSound;
        audioSource.loop = true; 
        audioSource.Play();
    }

    private void PlayLoseSound()
    {
        audioSource.clip = loseSound;
        audioSource.loop = false;  
        audioSource.Play();
    }
}
