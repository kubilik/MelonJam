using UnityEngine;

public class EmptyCupPickup : MonoBehaviour
{
    public IngredientType type = IngredientType.EmptyCup;
    public GameObject visualPrefab;

    [HideInInspector] public CupDispenser originatingDispenser;
}
