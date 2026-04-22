using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public GameObject trashPrefab;
    public Transform spawnPoint;
    public bool notTrash = true;

    public void SpawnTrash()
    {
        if (trashPrefab != null && spawnPoint != null)
        {
            notTrash = false;
            Instantiate(trashPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
