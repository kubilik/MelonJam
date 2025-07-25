using UnityEngine;

public class CupPlacementPoint : MonoBehaviour
{
    public GameObject placedCup; // Physical cup in the scene

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

    void Update()
    {
        // OPTIONAL SAFETY CHECK:
        // If the placedCup was destroyed externally, but reference not cleared
        if (placedCup != null && placedCup.Equals(null))
        {
            placedCup = null;
        }
    }
}
