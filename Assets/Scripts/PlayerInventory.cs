using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasOrder = false;
    public GameObject orderVisualPrefab;
    private GameObject orderInstance;

    public Transform handTransform;

    public void PickUpOrder()
    {
        if (hasOrder) return;

        hasOrder = true;
        Debug.Log("Order picked up.");

        if (orderVisualPrefab != null && handTransform != null)
        {
            orderInstance = Instantiate(orderVisualPrefab, handTransform.position, handTransform.rotation, handTransform);
        }
    }

    public void DeliverOrder()
    {
        if (!hasOrder) return;

        hasOrder = false;
        Debug.Log("Order delivered.");

        if (orderInstance != null)
        {
            Destroy(orderInstance);
        }
    }

    public bool IsHoldingOrder()
    {
        return hasOrder;
    }
}
