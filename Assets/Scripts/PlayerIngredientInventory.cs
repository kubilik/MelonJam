using UnityEngine;

public class PlayerIngredientInventory : MonoBehaviour
{
    private IngredientType currentType = IngredientType.None;
    private GameObject carriedObject;

    public Transform carryPoint;

    public bool IsCarrying()
    {
        return currentType != IngredientType.None || carriedObject != null;
    }

    public IngredientType GetCurrentType()
    {
        return currentType;
    }

    public void PickUpIngredient(IngredientType type, GameObject visualPrefab)
    {
        if (IsCarrying())
        {
            Debug.LogWarning("Already carrying something.");
            return;
        }

        currentType = type;

        if (visualPrefab != null)
        {
            carriedObject = Instantiate(visualPrefab, carryPoint.position, carryPoint.rotation, carryPoint);
        }
    }

    public IngredientType DropIngredient()
    {
        if (carriedObject != null)
        {
            Destroy(carriedObject);
            carriedObject = null;
        }

        IngredientType droppedType = currentType;
        currentType = IngredientType.None;

        return droppedType;
    }
}
