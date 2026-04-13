using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class PlateAssembly : MonoBehaviour
{
    [Header("Parent For Attached Ingredients")]
    public Transform ingredientAnchor;

    [Header("Recipe Order")]
    public IngredientType[] recipeOrder;

    [Header("Snap Points In Matching Order")]
    public Transform[] snapPoints;

    [Header("State")]
    public bool isComplete = false;

    private List<IngredientItem> attachedIngredients = new List<IngredientItem>();

    public bool TryAttachIngredient(IngredientItem ingredient)
    {
        if (ingredient == null)
            return false;

        if (ingredient.isAttached)
            return false;

        if (ingredientAnchor == null)
            return false;

        if (recipeOrder == null || recipeOrder.Length == 0)
            return false;

        if (snapPoints == null || snapPoints.Length == 0)
            return false;

        int nextIndex = attachedIngredients.Count;

        if (nextIndex >= recipeOrder.Length)
            return false;

        if (nextIndex >= snapPoints.Length)
            return false;

        IngredientType requiredType = recipeOrder[nextIndex];

        if (ingredient.ingredientType != requiredType)
            return false;

        Transform snapPoint = snapPoints[nextIndex];

        if (snapPoint == null)
            return false;

        Quaternion snapRotation = snapPoint.rotation * Quaternion.Euler(ingredient.snapLocalEulerAngles);

        ingredient.AttachToPlate(
            ingredientAnchor,
            snapPoint.position,
            snapRotation
        );

        attachedIngredients.Add(ingredient);

        CheckIfComplete();

        return true;
    }

    private void CheckIfComplete()
    {
        isComplete = attachedIngredients.Count == recipeOrder.Length;
    }

    public int GetAttachedCount()
    {
        return attachedIngredients.Count;
    }

    public bool IsComplete()
    {
        return isComplete;
    }
}