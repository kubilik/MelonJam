using UnityEngine;

public enum IngredientType
{
    None,
    Tortilla,
    Filling,
    Topping
}

public class IngredientItem : MonoBehaviour
{
    public IngredientType ingredientType;
}
