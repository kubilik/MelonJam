using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private bool playerInRange = false;
    private PlayerIngredientInventory playerInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerIngredientInventory>();
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
            if (playerInventory != null && playerInventory.IsCarrying())
            {
                IngredientType dropped = playerInventory.DropIngredient();
                Debug.Log("Discarded in trash: " + dropped);
            }
        }
    }
}
