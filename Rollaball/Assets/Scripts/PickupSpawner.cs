using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    
    public GameObject pickupPrefab;
    
    public float spawnRangeX = 10.0f;
    public float spawnHeightZ = 10.0f;
    
    public int pickupsToWin = 12;
    private int totalSpawned = 0;
    
    private bool isSpawnEnabled = true;

    private int maxAttemps = 30; //max attempts to spawn pickup
    
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
        if (!isSpawnEnabled || totalSpawned >= pickupsToWin) return;
        
        for (int i = 0; i < maxAttemps; i++) 
        {
            // Generate random position
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX),
                0.5f,
                Random.Range(-spawnHeightZ, spawnHeightZ)
            );
            
            // Check for overlapping objects at spawn position
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 0.5f);
            bool positionIsClear = true;
            
            // Check each detected collider
            foreach (Collider collider in colliders)
            {
                // If we find anything that's not ground, position is not valid
                if (!collider.CompareTag("Ground"))
                {
                    positionIsClear = false;
                    break;
                }
            }
            // If position is valid, spawn the pickup
            if (positionIsClear)
            {
                Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
                totalSpawned++;
                return;
            }
        }

        Debug.LogWarning("Could not find clear position to spawn pickup");
    }



    
    public void StopSpawning()
    {
        isSpawnEnabled = false;
    }
}

