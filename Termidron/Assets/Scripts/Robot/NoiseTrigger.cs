using UnityEngine;

/// <summary>
/// Detecta al jugador a un area
/// </summary>
public class NoiseTrigger : MonoBehaviour
{
    
    /// <param name="position">La posición en la que se genera el ruido</param>
    public void TriggerNoise(Vector3 position)
    {
        RobotFSM[] robots = FindObjectsOfType<RobotFSM>();

        foreach (RobotFSM robot in robots)
        {
            robot.OnPlayerMakesNoise(position);
        }
    }

    /// <summary>
    /// Detecta cuando "Player" entra en el area del trigger
    /// y genera ruido en la posicion del objeto
    /// </summary>
    /// <param name="other">El collider que entro en el trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerNoise(transform.position);
        }
    }
}
