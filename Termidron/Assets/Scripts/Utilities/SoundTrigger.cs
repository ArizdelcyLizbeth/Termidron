using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip soundClip; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
        }
    }
}
