using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
  public static GameManagement Instance;
  public List<Transform> availableBlocks;
  int winTime;
  int loseTime;
  int gold;
  int diamond;
  private Color newColor; // Màu mới mà bạn muốn đặt cho Albedo
  private Renderer meshRenderer;
  //public int numOfBots;
  public int numOfEnemys = 1;
  public bool inReadyTime = true;
  void Start()
  {
    meshRenderer = GetComponent<Renderer>();

        // Kiểm tra xem có Renderer hay không trước khi thay đổi màu
        if (meshRenderer != null)
        {
            // Thay đổi màu Albedo của material
            meshRenderer.material.color = newColor;
        }
    
  }
 
     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  
  void Update()
  {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
            availableBlocks.Clear();
            foreach (GameObject block in blocks)
            {
                BlockController blockController = block.GetComponent<BlockController>();
                if (!blockController.isHolded)
                {
                    availableBlocks.Add(block.transform);
                }
            }
  }
  public IEnumerator DelayedFunction()
    { 
        yield return new WaitForSeconds(3f);
    }
  
}
