using UnityEngine;

public class CupPlacementPoint : MonoBehaviour
{
    public GameObject placedCup; // Sahnedeki fiziksel bardak

    public bool HasCup()
    {
        return placedCup != null;
    }

    public void PlaceCup(GameObject cup)
    {
        placedCup = cup;
        cup.transform.position = transform.position;
        cup.transform.rotation = transform.rotation;
    }

    public void ClearCup()
    {
        placedCup = null;
    }

    public void RemoveCup()
    {
        if (placedCup != null)
        {
            Destroy(placedCup);
            placedCup = null;
        }
    }
}
