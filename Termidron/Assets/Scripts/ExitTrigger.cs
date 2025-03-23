using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public KeyCollector keyCollector;
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (
            collision.collider.CompareTag("ExitDoor") &&
            keyCollector.HasCollectedAllKeys() &&
            gameManager.IsTimeRemaining()
        )
        {
            gameManager.FinishGame();
        }
    }
}