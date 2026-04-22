using System.Runtime.CompilerServices;
using UnityEngine;

public class EatScript : MonoBehaviour
{
    public bool canEatThis;
    public FoodScript foodScript;
    private void OnTriggerEnter(Collider other)
    {;
        if (other.CompareTag("Food"))
        {
            canEatThis = true;
            foodScript = other.GetComponent<FoodScript>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            canEatThis = false;
            foodScript = null;
        }
    }
}
