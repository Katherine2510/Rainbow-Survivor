using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEnemy : MonoBehaviour
{
    public List<Transform> randEnemy;
    public GameObject EnemyPrefab;

    void Start()
    {
        if (randEnemy.Count >= GameManagement.Instance.numOfEnemys)
        {
            List<int> selectedIndices = new List<int>();

            // Đặt ba khối
            for (int i = 0; i < GameManagement.Instance.numOfEnemys; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, randEnemy.Count);
                } while (selectedIndices.Contains(randomIndex)); // Kiểm tra xem chỉ số đã được chọn trước đó chưa
                selectedIndices.Add(randomIndex);
                GameObject newEnemy = Instantiate(EnemyPrefab,new Vector3(randEnemy[randomIndex].position.x, randEnemy[randomIndex].position.y + 1.2f, randEnemy[randomIndex].position.z), Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("Not enough selected points to place Enemys.");
        }
    }
}
