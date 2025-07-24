using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasOrder = false;

    public void PickUpOrder()
    {
        hasOrder = true;
        Debug.Log("Order picked up.");
    }

    public void DeliverOrder()
    {
        hasOrder = false;
        Debug.Log("Order delivered.");
    }

    public bool IsHoldingOrder()
    {
        return hasOrder;
    }
}
