using UnityEngine;
//using TMPro; 

public class KeyCollector : MonoBehaviour
{
    // Tendremos que resetear este valor a 0.
    public Display1 display;
    private int keysCollected = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) 
        {
            this.keysCollected++;
            display.UpdateKeysCounter(keysCollected);
            other.gameObject.SetActive(false);
        }
    }

    public void ResetKeysCollected()
    {
        this.keysCollected =  0;
    }

    public bool HasCollectedAllKeys()
    {
        return keysCollected == 3;
    }
}