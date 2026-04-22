using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SlowZone : MonoBehaviour
{
    public DynamicMoveProvider moveProvider;

    public float slowSpeed = 1f;
    private float originalSpeed;

    private void Start()
    {
        if (moveProvider == null)
        {
            return;
        }

        originalSpeed = moveProvider.moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        moveProvider.moveSpeed = slowSpeed;
    }

    private void OnTriggerExit(Collider other)
    {
        moveProvider.moveSpeed = originalSpeed;
    }
}