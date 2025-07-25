using UnityEngine;

public class PlayerIngredientInventory : MonoBehaviour
{
    public Transform holdPoint; // Where the item appears in hand
    private IngredientType carriedType = IngredientType.None;
    private GameObject heldVisual;

    public bool IsCarrying()
    {
        return carriedType != IngredientType.None;
    }

    public IngredientType PeekIngredient()
    {
        return carriedType;
    }

    public void PickUpIngredient(IngredientType type, GameObject visualPrefab)
    {
        if (IsCarrying())
        {
            Debug.LogWarning("Already carrying an item!");
            return;
        }

        carriedType = type;

        if (visualPrefab != null)
        {
            heldVisual = Instantiate(visualPrefab, holdPoint.position, holdPoint.rotation, holdPoint);
        }
    }

    public IngredientType DropIngredient()
    {
        if (!IsCarrying()) return IngredientType.None;

        if (heldVisual != null)
        {
            Destroy(heldVisual);
        }

        IngredientType droppedType = carriedType;
        carriedType = IngredientType.None;
        heldVisual = null;

        return droppedType;
    }
}
