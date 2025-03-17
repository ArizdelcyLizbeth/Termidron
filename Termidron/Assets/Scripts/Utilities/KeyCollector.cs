using UnityEngine;
using TMPro; 

public class KeyCollector : MonoBehaviour
{
    private int keysCollected = 0; 
    public TMP_Text messageText; 

    void Start()
    {
        messageText.text = ""; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) 
        {
            keysCollected++;
            Destroy(other.gameObject); 
            messageText.text = "Agarraste una llave. Llaves: " + keysCollected + "/3";

            if (keysCollected == 3)
            {
                messageText.text = "¡Tienes todas las llaves! Dirígete a la salida, rápido.";
            }
        }
    }

    public int GetKeyCount()
    {
        return keysCollected;
    }
}
