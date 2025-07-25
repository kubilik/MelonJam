using UnityEngine;
using System.Collections.Generic;

public class TacoBuilder : MonoBehaviour
{
    public IngredientType[] requiredIngredients = {
        IngredientType.Tortilla,
        IngredientType.Filling,
        IngredientType.Topping
    };

    public GameObject finishedTacoPrefab;
    public Transform finalSpawnPoint;

    public GameObject tortillaPrefab;
    public GameObject fillingPrefab;
    public GameObject toppingPrefab;

    public Transform[] spawnPoints; // length = 3

    private List<IngredientType> currentIngredients = new List<IngredientType>();
    private List<GameObject> spawnedVisuals = new List<GameObject>();

    public void AddIngredient(IngredientType type)
    {
        if (currentIngredients.Count >= requiredIngredients.Length)
        {
            Debug.Log("Too many ingredients.");
            return;
        }

        currentIngredients.Add(type);

        // Spawn visual to next point
        GameObject visual = GetPrefabForIngredient(type);
        if (visual != null && currentIngredients.Count - 1 < spawnPoints.Length)
        {
            Transform spawnPoint = spawnPoints[currentIngredients.Count - 1];
            GameObject visualInstance = Instantiate(visual, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            spawnedVisuals.Add(visualInstance);
        }

        // Check if complete
        if (currentIngredients.Count == requiredIngredients.Length)
        {
            if (IsCorrectCombination())
            {
                Debug.Log("Taco completed!");

                Instantiate(finishedTacoPrefab, finalSpawnPoint.position, finalSpawnPoint.rotation);
            }
            else
            {
                Debug.Log("Wrong combination. Resetting...");
            }

            ResetBuilder();
        }
    }

    private GameObject GetPrefabForIngredient(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Tortilla:
                return tortillaPrefab;
            case IngredientType.Filling:
                return fillingPrefab;
            case IngredientType.Topping:
                return toppingPrefab;
            default:
                return null;
        }
    }

    private bool IsCorrectCombination()
    {
        IngredientType[] sortedRequired = (IngredientType[])requiredIngredients.Clone();
        IngredientType[] sortedCurrent = currentIngredients.ToArray();

        System.Array.Sort(sortedRequired);
        System.Array.Sort(sortedCurrent);

        for (int i = 0; i < sortedRequired.Length; i++)
        {
            if (sortedRequired[i] != sortedCurrent[i])
                return false;
        }

        return true;
    }

    private void ResetBuilder()
    {
        currentIngredients.Clear();

        foreach (GameObject obj in spawnedVisuals)
        {
            if (obj != null)
                Destroy(obj);
        }

        spawnedVisuals.Clear();
    }
}
