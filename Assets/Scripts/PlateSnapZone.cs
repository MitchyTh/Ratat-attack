using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlateSnapZone : MonoBehaviour
{
    public PlateAssembly plateAssembly;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void TrySnapIngredient(IngredientItem ingredient)
    {
        if (plateAssembly == null)
            return;

        plateAssembly.TryAttachIngredient(ingredient);
    }

    private void OnTriggerEnter(Collider other)
    {
        IngredientItem ingredient = other.GetComponentInParent<IngredientItem>();

        if (ingredient != null && !ingredient.isAttached)
        {
            ingredient.SetCurrentSnapZone(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IngredientItem ingredient = other.GetComponentInParent<IngredientItem>();

        if (ingredient != null && !ingredient.isAttached)
        {
            ingredient.SetCurrentSnapZone(null);
        }
    }
}