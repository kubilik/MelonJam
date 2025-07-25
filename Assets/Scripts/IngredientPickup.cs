using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    public IngredientType type;
    public GameObject visualPrefab;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            var inv = other.GetComponent<PlayerIngredientInventory>();
            if (inv != null && !inv.IsCarrying())
            {
                inv.PickUpIngredient(type, visualPrefab);
                Debug.Log("Picked up: " + type);
            }
        }
    }
}
