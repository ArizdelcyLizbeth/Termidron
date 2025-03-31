using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    public void TriggerNoise(Vector3 position)
    {
        RobotFSM[] robots = FindObjectsOfType<RobotFSM>();

        foreach (RobotFSM robot in robots)
        {
            robot.OnPlayerMakesNoise(position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerNoise(transform.position);
        }
    }
}
