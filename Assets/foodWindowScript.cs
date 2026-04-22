using UnityEngine;

public class foodWindowScript : MonoBehaviour
{
    public BoxCollider foodWindowCollider;
    public GameManagerScript gameManager;
    void Start()
    {
        foodWindowCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WhitePlate"))
        {
            PlateAssembly plateAssembly = other.GetComponent<PlateAssembly>();
            if (plateAssembly.isComplete == true)
            {
                gameManager.increaseScore(100);
            }
            plateAssembly.destroyPlate();
        }
    }
}
