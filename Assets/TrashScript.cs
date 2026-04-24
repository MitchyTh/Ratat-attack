using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public GameObject trashPrefab;
    public Transform spawnPoint;
    public bool trashThere = true;

    public void SpawnTrash()
    {
        if (trashPrefab != null && spawnPoint != null)
        {
            trashThere = false;
            Instantiate(trashPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
