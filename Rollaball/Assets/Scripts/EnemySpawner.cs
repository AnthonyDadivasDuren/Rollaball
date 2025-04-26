using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    //Enemy Prefab to spawn
    public GameObject enemyPrefab;
    
    // reference to the player game object
    public GameObject player;

    // time between spawns
    public float spawnInterval = 5.0f;
    
    // Timer for tracking spawn cooldown
    private float spawnTimer;
    
    // Boundaries for random spawn position
    public float spawnRangeX = 10.0f;
    public float spawnHeightZ = 10.0f;

    public int maxEnemies = 5;
    
    
    private bool isSpawnEnabled = true;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawnEnabled) return;
        
        spawnTimer -= Time.deltaTime;
        
        // Count current enemies
        GameObject[] currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        // stops code from running if max enemies is reached
        if (currentEnemies.Length >= maxEnemies + 1)
        {
            // Reset timer if at max enemies to maintain spawn rate when an enemy is destroyed
            spawnTimer = spawnInterval;
            return;
        }
        
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // Generate random position
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnHeightZ, spawnHeightZ);
        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ);
        
        // Create the enemy 
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        // Get the EnemyMovement component and set the player reference
        EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null && player != null)
        {
            enemyMovement.player = player.transform;
        }
    }

    public void StopSpawning()
    {
        isSpawnEnabled = false;
    }
}

