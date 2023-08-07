
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    public Transform targetZone;
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform holdingPosition;
    private bool isHoldingBlocks = false;
    Vector3 positionBlock = Vector3.zero;
    public string blockTag = "Block";
    public float distanceThreshold = 10f;
    public float heightOfBlock = 1f;
    public GameObject _characterController;
    public int _doneBlocks = 10;
    public Text DoneBlock;
    public Transform Enemy;
    public bool canRandom  = true;
    public List<Transform> availableBlocks;
    Transform randomBlock;
    Transform pickedBlock;
    public Transform goOutPoint;
    private float timer;
    [SerializeField] private float wanderRadius = 1000f;
    [SerializeField] private float wanderTimer = 3f;
    private Vector3 targetPosition;
     
   private void Start()
    {   //Get a list of all the blocks that are not picked up and not in the target zone
        agent = GetComponent<NavMeshAgent>();
        holdingPosition = transform.Find("HoldingPosition");   
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, targetZone.position) <= 1f && isHoldingBlocks)
        {
            ReleaseBlocks();
            canRandom = true;
            isHoldingBlocks = false;
        }
        if (GameManagement.Instance != null && GameManagement.Instance.inReadyTime  && timer > wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0f;
        }
        if (!isHoldingBlocks)
        {    
            if (GameManagement.Instance != null)
            {
                availableBlocks = GameManagement.Instance.availableBlocks;
            }

            if (availableBlocks.Count > 0 && canRandom && !GameManagement.Instance.inReadyTime)
            {
                if (availableBlocks.Count > 0)
                {
                    int randomBlockIndex = Random.Range(0, availableBlocks.Count);
                    
                    if (availableBlocks[randomBlockIndex] != null) {
                        randomBlock = availableBlocks[randomBlockIndex];
                        targetPosition = randomBlock.transform.position;
                        agent.SetDestination(targetPosition);
                        canRandom = false;
                    } else {
                        canRandom = true;
                    }
               }
            }
            if (availableBlocks.Count <= 0 && timer > wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0f;
            }
            if (Vector3.Distance(transform.position, targetPosition) < 3f)
            { 
                if (randomBlock != null) {
                    canRandom = true;
                    holdingBlocks(randomBlock.transform);
                } else {
                    canRandom = true;
                    Debug.Log("NULLLLLL");
                }
                
            }
        }
    }
     public void holdingBlocks(Transform blockTransform)
    {
       
        BlockController blockController = blockTransform.GetComponent<BlockController>();
        //Debug.Log(blockController.isHolded);
        if (blockController.isHolded ) {
            //Debug.Log("Mất khối hộp");
        }
        if (!blockController.isHolded && blockTransform != null) {
            blockTransform.SetParent(transform);
            blockTransform.localPosition = new Vector3(0f, 0.25f, -0.04f);
            blockTransform.localRotation = Quaternion.Euler(Vector3.zero);    
            blockController.isHolded = true;
            isHoldingBlocks = true;
            pickedBlock = blockTransform;
            agent.SetDestination(targetZone.position);

        } else {
             
        }
       
   }
    private void ReleaseBlocks()
    {
        var targetposition = new Vector3(Random.Range(targetZone.position.x - 4.0f, targetZone.position.x + 4.0f), targetZone.position.y  + heightOfBlock/2f ,Random.Range(targetZone.position.z -4.0f, targetZone.position.z + 4.0f));
        pickedBlock.SetParent(targetZone);
        pickedBlock.position = targetposition;
        isHoldingBlocks = false;
        positionBlock = Vector3.zero;
        _characterController.GetComponent<CharacterMovement>().score += 1;
    //Debug.Log(_characterController.GetComponent<CharacterMovement>().score);
    DoneBlock.text = _characterController.GetComponent<CharacterMovement>().score + "/" + _doneBlocks; 
    }
    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
 
}