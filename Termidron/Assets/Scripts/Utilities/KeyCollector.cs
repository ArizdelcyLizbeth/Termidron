using UnityEngine;

/// <summary>
/// Controlador para la recolección de llaves y el manejo de los corazones del jugador.
/// También se encarga de actualizar la interfaz de usuario (UI) con los contadores de llaves y corazones.
/// </summary>
public class KeyCollector : MonoBehaviour
{
    public Display1 display;
    private int keysCollected = 0;
    private int hearts = 5;
    public GameManager gameManager;

    /// <summary>
    /// Detecta la entrada del jugador en un área de colisión con una llave.
    /// Aumenta el contador de llaves y desactiva el objeto de la llave.
    /// </summary>
    /// <param name="other">Objeto con el que el jugador ha colisionado.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) 
        {
            this.keysCollected++;
            other.gameObject.SetActive(false);
            display.UpdateKeysCounter(keysCollected);
        }
    }

    /// <summary>
    /// Detecta la colisión del jugador con un objeto, como un "Hazard" (peligro).
    /// Reduce el número de corazones del jugador y actualiza el contador en la UI.
    /// Si los corazones llegan a 0, termina el juego.
    /// </summary>
    /// <param name="collision">Información de la colisión ocurrida.</param>
    void OnCollisionEnter(Collision collision)
    {
        if (
            collision.collider.CompareTag("Hazard") &&
            hearts >= 2
        )
        {
            this.hearts--;
            display.UpdateHeartsCounter(hearts);
        }
        else if (collision.collider.CompareTag("Hazard")) 
        {
            gameManager.FinishGame("FIN DEL CAMINO", "Tus corazones latieron por última vez... y la oscuridad se apoderó de tu aventura.");
        }
    }

    /// <summary>
    /// Resetea los contadores de llaves y corazones al inicio de una nueva partida.
    /// </summary>
    public void ResetKeysCollected()
    {
        this.keysCollected =  0;
        this.hearts = 5;
    }

    /// <summary>
    /// Verifica si el jugador ha recolectado todas las llaves necesarias (3 llaves).
    /// </summary>
    /// <returns>True si el jugador ha recolectado todas las llaves, de lo contrario false.</returns>
    public bool HasCollectedAllKeys()
    {
        return keysCollected == 3;
    }
}