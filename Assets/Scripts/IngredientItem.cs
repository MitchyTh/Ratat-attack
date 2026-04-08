using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class IngredientItem : MonoBehaviour
{
    public IngredientType ingredientType;

    [HideInInspector] public PlateSnapZone currentSnapZone;
    [HideInInspector] public bool isAttached = false;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (isAttached)
            return;

        if (currentSnapZone != null)
        {
            currentSnapZone.TryAttachIngredient(this);
        }
    }

    public void AttachToPlate(Transform parentTransform, Vector3 localPosition, Quaternion localRotation)
    {
        isAttached = true;

        transform.SetParent(parentTransform);
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }
    }
}