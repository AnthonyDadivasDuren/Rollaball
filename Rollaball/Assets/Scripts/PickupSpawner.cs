using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    
    public GameObject pickupPrefab;
    
    public float spawnRangeX = 10.0f;
    public float spawnHeightZ = 10.0f;
    
    public int pickupsToWin = 12;
    private int totalSpawned = 0;
    
    private bool isSpawnEnabled = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPickup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPickup()
    {
        if (!isSpawnEnabled) return;
        if (totalSpawned >= pickupsToWin) return;
        
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnHeightZ, spawnHeightZ);
        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ);
        
        GameObject newPickup = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        totalSpawned++;
    }
    
    public void StopSpawning()
    {
        isSpawnEnabled = false;
    }
}

