using System.Collections.Generic;
using UnityEngine;

public class PlateSnapZone : MonoBehaviour
{
    [Header("References")]
    public Transform stackPoint;

    [Header("Recipe Order")]
    public IngredientType[] recipeOrder;

    [Header("Stack Heights")]
    public float[] stackHeights;

    private List<IngredientItem> attachedIngredients = new List<IngredientItem>();

    private void OnTriggerEnter(Collider other)
    {
        IngredientItem ingredient = other.GetComponentInParent<IngredientItem>();
        if (ingredient != null && !ingredient.isAttached)
        {
            ingredient.currentSnapZone = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IngredientItem ingredient = other.GetComponentInParent<IngredientItem>();
        if (ingredient != null && ingredient.currentSnapZone == this)
        {
            ingredient.currentSnapZone = null;
        }
    }

    public bool TryAttachIngredient(IngredientItem ingredient)
    {
        if (ingredient == null)
            return false;

        if (ingredient.isAttached)
            return false;

        int nextIndex = attachedIngredients.Count;

        if (nextIndex >= recipeOrder.Length)
        {
            Debug.Log("Plate is already full.");
            return false;
        }

        if (ingredient.ingredientType != recipeOrder[nextIndex])
        {
            Debug.Log("Wrong ingredient. Expected: " + recipeOrder[nextIndex]);
            return false;
        }

        float yOffset = 0f;

        if (nextIndex < stackHeights.Length)
        {
            yOffset = stackHeights[nextIndex];
        }

        Vector3 localSnapPosition = new Vector3(0f, yOffset, 0f);
        Quaternion localSnapRotation = Quaternion.identity;

        ingredient.AttachToPlate(stackPoint, localSnapPosition, localSnapRotation);
        attachedIngredients.Add(ingredient);
        ingredient.currentSnapZone = null;

        Debug.Log("Attached: " + ingredient.ingredientType);

        return true;
    }

    public bool IsBurgerComplete()
    {
        return attachedIngredients.Count == recipeOrder.Length;
    }
}