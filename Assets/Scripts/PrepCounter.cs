using UnityEngine;

public class PrepCounter : MonoBehaviour
{
    public TacoBuilder builder;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            var inv = other.GetComponent<PlayerIngredientInventory>();
            if (inv != null && inv.IsCarrying())
            {
                IngredientType dropped = inv.DropIngredient();
                builder.AddIngredient(dropped);
                Debug.Log("Placed on counter: " + dropped);
            }
        }
    }
}
