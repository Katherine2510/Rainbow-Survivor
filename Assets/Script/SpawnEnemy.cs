
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
   public GameObject enemyPrefab;
   public float spawnRate = 2f;
   public float spawnRadius = 5f;
   private float spawnTimer = 0f;

   void Update()
   {
    spawnTimer += Time.deltaTime;
    if (spawnTimer >= spawnRate) {
        if (enemyPrefab != null) {

        }
    }
     if (spawnTimer >= spawnRate)
        {
            SpawnEnemyFunc();
            spawnTimer = 0f; // Reset spawnTimer để bắt đầu đếm lại
        }
    } 

    void SpawnEnemyFunc() {
        Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
         Vector3 spawnPosition = transform.position + randomPosition;

        // Sinh quái vật tại vị trí spawnPosition
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
