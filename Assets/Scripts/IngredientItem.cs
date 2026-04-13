using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class IngredientItem : MonoBehaviour
{
    [Header("Ingredient")]
    public IngredientType ingredientType;

    [Header("Snap Rotation On Plate")]
    public Vector3 snapLocalEulerAngles;

    [HideInInspector] public bool isAttached = false;

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private PlateSnapZone currentSnapZone;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    public void SetCurrentSnapZone(PlateSnapZone zone)
    {
        currentSnapZone = zone;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (isAttached)
            return;

        if (currentSnapZone != null)
        {
            currentSnapZone.TrySnapIngredient(this);
        }
    }

    public void AttachToPlate(Transform parent, Vector3 worldPosition, Quaternion worldRotation)
    {
        isAttached = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        transform.SetParent(parent, true);
        transform.position = worldPosition;
        transform.rotation = worldRotation;

        grabInteractable.enabled = false;
    }
}