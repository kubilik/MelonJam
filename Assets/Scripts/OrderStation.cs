using UnityEngine;

public class OrderStation : MonoBehaviour
{
    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null && !playerInventory.IsHoldingOrder())
            {
                playerInventory.PickUpOrder();
                Debug.Log("Order created.");
            }
        }
    }
}
