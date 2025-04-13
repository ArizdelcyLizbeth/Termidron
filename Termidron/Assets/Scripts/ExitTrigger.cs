using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detecta la colisión del jugador con la puerta de salida y verifica si cumple con las condiciones para finalizar el juego exitosamente.
/// </summary>
public class ExitTrigger : MonoBehaviour
{
    public KeyCollector keyCollector;
    public GameManager gameManager;

    /// <summary>
    /// Se llama automáticamente por Unity cuando este GameObject colisiona con otro.
    /// Verifica si el jugador ha cumplido las condiciones necesarias para finalizar el juego exitosamente.
    /// </summary>
    /// <param name="collision">Información sobre la colisión detectada.</param>
    void OnCollisionEnter(Collision collision)
    {
        if (
            collision.collider.CompareTag("ExitDoor") &&
            keyCollector.HasCollectedAllKeys() &&
            gameManager.IsTimeRemaining()
        )
        {
            gameManager.FinishGame("!LEYENDA INMORTAL!", "Los libros contarán tu historia. Rápido, astuto y sin miedo. !Eres un verdadero campeón!");
        }
    }
}