using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music Prefabs")]
    public GameObject menuMusicPrefab;
    public GameObject gameMusicPrefab;

    private GameObject currentMusicObject;
    private bool lastState;

    void Start()
    {
        // Set initial state and spawn starting music
        if (GameManagerScript.Instance != null)
        {
            lastState = GameManagerScript.Instance.gameStarted;
            SpawnMusic(lastState ? gameMusicPrefab : menuMusicPrefab);
        }
    }

    void Update()
    {
        if (GameManagerScript.Instance == null) return;

        bool currentState = GameManagerScript.Instance.gameStarted;

        // Only switch if the state has actually changed since the last frame
        if (currentState != lastState)
        {
            lastState = currentState;
            GameObject prefabToSpawn = currentState ? gameMusicPrefab : menuMusicPrefab;
            SpawnMusic(prefabToSpawn);
        }
    }

    void SpawnMusic(GameObject prefab)
    {
        // 1. Remove the old music object if it exists
        if (currentMusicObject != null)
        {
            Destroy(currentMusicObject);
        }

        // 2. Create the new music object
        if (prefab != null)
        {
            currentMusicObject = Instantiate(prefab, transform.position, Quaternion.identity);

            // Optional: Make the music follow this manager or stay alive through scene loads
            currentMusicObject.transform.SetParent(this.transform);
        }
    }
}