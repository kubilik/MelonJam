using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    public IngredientType type;
    public GameObject visualPrefab;
    public CupDispenser originatingDispenser; // Only needed for cups

    void OnDestroy()
    {
        if (originatingDispenser != null)
        {
            originatingDispenser.ClearCup();
        }
    }
}
