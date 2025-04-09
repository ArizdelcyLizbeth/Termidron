using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    public Transform swordAttachPoint; 
    private GameObject sword; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && sword == null)
        {
            sword = other.gameObject;
            AttachSwordToPlayer(sword); 
        }
    }

    void AttachSwordToPlayer(GameObject sword)
    {
        Rigidbody rb = sword.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; 
        }

        Collider swordCollider = sword.GetComponent<Collider>();
        Collider handCollider = swordAttachPoint.GetComponent<Collider>();
        if (swordCollider != null && handCollider != null)
        {
            Physics.IgnoreCollision(swordCollider, handCollider); 
        }

        sword.transform.SetParent(swordAttachPoint);
        sword.transform.localPosition = Vector3.zero;
        sword.transform.localRotation = Quaternion.identity;
    }

    public void DestroySword()
    {
        if (sword != null)
        {
            Destroy(sword);
            sword = null; 
        }
    }

    public void DropSword()
    {
        if (sword != null)
        {
            Rigidbody rb = sword.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Collider swordCollider = sword.GetComponent<Collider>();
            Collider handCollider = swordAttachPoint.GetComponent<Collider>();
            if (swordCollider != null && handCollider != null)
            {
                Physics.IgnoreCollision(swordCollider, handCollider, false); 
            }

            sword.transform.SetParent(null); 
            sword = null; 
        }
    }
}
