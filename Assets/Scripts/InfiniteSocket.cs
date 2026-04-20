using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class InfiniteSocket : MonoBehaviour
{
    [Header("References")]
    public XRSocketInteractor socket;
    public GameObject bunPrefab;

    [Header("Settings")]
    public float respawnDelay = 0.05f;

    private bool isRespawning = false;

    void Start()
    {
        SpawnBun();
    }

    void OnEnable()
    {
        socket.selectExited.AddListener(OnBunRemoved);
    }

    void OnDisable()
    {
        socket.selectExited.RemoveListener(OnBunRemoved);
    }

    private void OnBunRemoved(SelectExitEventArgs args)
    {
        if (!isRespawning)
        {
            isRespawning = true;
            Invoke(nameof(SpawnBun), respawnDelay);
        }
    }

    private void SpawnBun()
    {
        // If something already got placed back, do nothing
        if (socket.hasSelection)
        {
            isRespawning = false;
            return;
        }

        Instantiate(
            bunPrefab,
            socket.transform.position,
            socket.transform.rotation
        );

        isRespawning = false;
    }
}