using UnityEngine;

public class PlayerIngredientInventory : MonoBehaviour
{
    public IngredientType carriedIngredient = IngredientType.None;
    public GameObject visualPrefab;
    private GameObject instance;

    public Transform handTransform;

    public void PickUpIngredient(IngredientType type, GameObject prefab)
    {
        if (carriedIngredient != IngredientType.None) return;

        carriedIngredient = type;

        if (prefab != null && handTransform != null)
        {
            instance = Instantiate(prefab, handTransform.position, handTransform.rotation, handTransform);
        }
    }

    public IngredientType DropIngredient()
    {
        IngredientType dropped = carriedIngredient;
        carriedIngredient = IngredientType.None;

        if (instance != null)
        {
            Destroy(instance);
        }

        return dropped;
    }

    public bool IsCarrying()
    {
        return carriedIngredient != IngredientType.None;
    }

    public IngredientType GetCarried()
    {
        return carriedIngredient;
    }
}
