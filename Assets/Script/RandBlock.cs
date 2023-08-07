using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandBlock : MonoBehaviour
{
    public List<Transform> randBlock;
    public GameObject blockPrefab;

    void Start()
    {
        if (randBlock.Count >= 10)
        {
            List<int> selectedIndices = new List<int>();

            // Đặt ba khối
            for (int i = 0; i < 10; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, randBlock.Count);
                } while (selectedIndices.Contains(randomIndex)); // Kiểm tra xem chỉ số đã được chọn trước đó chưa
                selectedIndices.Add(randomIndex);
                GameObject newBlock = Instantiate(blockPrefab,new Vector3(randBlock[randomIndex].position.x, randBlock[randomIndex].position.y + 1.2f, randBlock[randomIndex].position.z), Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("Not enough selected points to place blocks.");
        }
    }
}
