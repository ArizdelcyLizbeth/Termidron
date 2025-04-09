using UnityEngine;
//using TMPro; 

public class KeyCollector : MonoBehaviour
{
    public Display1 display;
    private int keysCollected = 0;
    private int hearts = 5;
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) 
        {
            this.keysCollected++;
            other.gameObject.SetActive(false);
            display.UpdateKeysCounter(keysCollected);
        }
    }

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
            gameManager.FinishGame("FIN DEL CAMINO", "Tus corazones latieron por �ltima vez... y la oscuridad se apoder� de tu aventura.");
        }
    }

    public void ResetKeysCollected()
    {
        this.keysCollected =  0;
        this.hearts = 5;
    }

    public bool HasCollectedAllKeys()
    {
        return keysCollected == 3;
    }
}