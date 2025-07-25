using UnityEngine;

public class TacoBuilder : MonoBehaviour
{
    private IngredientType[] requiredOrder = {
        IngredientType.Tortilla,
        IngredientType.Filling,
        IngredientType.Topping
    };

    private int currentIndex = 0;

    public GameObject tacoPrefab;
    public Transform spawnPoint;

    public void AddIngredient(IngredientType type)
    {
        if (currentIndex < requiredOrder.Length && type == requiredOrder[currentIndex])
        {
            currentIndex++;
            Debug.Log("Correct ingredient placed: " + type);

            if (currentIndex == requiredOrder.Length)
            {
                Debug.Log("Taco completed!");
                Instantiate(tacoPrefab, spawnPoint.position, Quaternion.identity);
                currentIndex = 0; // Reset for next taco
            }
        }
        else
        {
            Debug.Log("Wrong ingredient. Resetting...");
            currentIndex = 0;
        }
    }
}
