using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    // Arrays for visual and hitbox spawns
    public GameObject[] spawnsVisual;
    public GameObject[] spawnsHitbox;

    public GameObject hitboxPrefab; // Assign a hitbox prefab in the Inspector
    public List<GameObject> cars; // Ensure this list is populated in the Inspector with your car prefabs

    private float spawnDelay = 1f; // Initial delay before first car spawn
    private float spawnIntervalMin = 1f; // Minimum interval between spawns
    private float spawnIntervalMax = 3f; // Maximum interval between spawns

    void Start()
    {
        // Start the spawning process
        Invoke("SpawnCar", spawnDelay);
    }

    void SpawnCar()
    {
        // Randomly select an index for the spawn location
        int selectedIndex = Random.Range(0, spawnsVisual.Length);

        // Ensure the selected index is valid for both arrays
        if (selectedIndex < spawnsVisual.Length && selectedIndex < spawnsHitbox.Length)
        {
            // Get the selected visual and hitbox spawns
            GameObject selectedVisualSpawn = spawnsVisual[selectedIndex];
            GameObject selectedHitboxSpawn = spawnsHitbox[selectedIndex];

            // Randomly select a car prefab
            GameObject carPrefab = cars[Random.Range(0, cars.Count)];

            // Spawn the car at the visual location
            Instantiate(carPrefab, selectedVisualSpawn.transform.position, selectedVisualSpawn.transform.rotation);

            // Spawn the hitbox at the hitbox location
            Instantiate(hitboxPrefab, selectedHitboxSpawn.transform.position, selectedHitboxSpawn.transform.rotation);
        }

        // Schedule the next car spawn
        float nextSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        Invoke("SpawnCar", nextSpawnInterval);
    }
}
