using UnityEngine;

public class cabinetScript : MonoBehaviour
{
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void makeKinematic()
    {
        rb.isKinematic = true;
    }
    public void undoKinematic()
    {
        rb.isKinematic = false;
    }
}
