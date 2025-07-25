using UnityEngine;

public class CupDispenser : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject emptyCupPrefab;

    private GameObject currentCup;

    public void DispenseCup()
    {
        if (currentCup == null)
        {
            currentCup = Instantiate(emptyCupPrefab, spawnPoint.position, spawnPoint.rotation);

            IngredientPickup pickup = currentCup.GetComponent<IngredientPickup>();
            if (pickup != null)
            {
                pickup.originatingDispenser = this; // �NEML�!
            }
        }
    }


    public bool HasCupReady()
    {
        return currentCup != null;
    }

    public void ClearCup()
    {
        currentCup = null;
    }

    public GameObject GetCurrentCup()
    {
        return currentCup;
    }

    //NEW: Used by interaction script to check if we can spawn
    public bool CanSpawnCup()
    {
        return currentCup == null;
    }
}
