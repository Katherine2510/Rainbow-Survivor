
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    private bool isHoldingBlock = false;
    private Transform holdPosition;
    private Animator anim;
    public int _doneBlocks = 10;
    public Transform Player;
    public Transform TargetZone;
    private int _okBlocks = 0;
    public bool isWin = false;
    [SerializeField] private GameController gameController;
    public Text DoneBlock;
    public float flySpeed = 5f;
    public List<Transform> pickedBlocks = new List<Transform>(); 
    Vector3 positionBlock = Vector3.zero;
    public string blockTag = "Block";
    public float distanceThreshold = 10f;
    public float heightOfBlock = 1f;
    private int markBlock = 0;
    private int blocksPickedUp = 0;
    public int score = 0;
    private bool isTargeZone = false;
   void Start() {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", false);
        DoneBlock.text = _okBlocks + "/" + _doneBlocks;   
        Vector3 pickedPosition = holdPosition.position; 
    }
    private void Awake()
    {
        holdPosition = transform.Find("Holding");       
    }
    private void Update()
    {
        //Debug.Log(isHoldingBlock);
        if (score == _doneBlocks && !isWin) {
                isWin = true;
                anim.SetBool("isWin", true);
        }
        Move();
        //thắng game + delay time + set position of player
        if (isWin)
        {
        Player.position = new Vector3( TargetZone.position.x, TargetZone.position.y + heightOfBlock/2f, TargetZone.position.z) ;
        gameController.StartDelayBeforeWin();
        }
    }
    private void Move()
    {
         if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
      
            anim.SetBool("isRunning", true);
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            anim.SetBool("isRunning", false);
        }
    }  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            
            StartCoroutine(holdingBlocks(other.transform));
             
        }
        if (isHoldingBlock && other.CompareTag("TargetZone"))
        {   
            positionBlock = Vector3.zero;
            isTargeZone = true;
            RealeaseBlock(pickedBlocks);
            
        }
    }
   public IEnumerator holdingBlocks(Transform blockTransform)
    {
        BlockController blockController = blockTransform.GetComponent<BlockController>();
        if (blockController != null && !blockController.isHolded )
        {           
            isHoldingBlock = true;
            pickedBlocks.Add(blockTransform);
            blockTransform.SetParent(holdPosition);
            blockTransform.localRotation = Quaternion.Euler(Vector3.zero);
            Debug.Log("Size" + pickedBlocks.Count);
            blocksPickedUp++;
            if (blocksPickedUp % 2 == 1)
            {
                blockTransform.localPosition = positionBlock;
            }
            else if (blocksPickedUp % 2 == 0)
            {
                Debug.Log("mark" + markBlock);
                Transform block1 = pickedBlocks[markBlock];
                Vector3 block1LocalPos = new Vector3(positionBlock.x + 0.03f, positionBlock.y , positionBlock.z);
                Vector3 block2LocalPos = new Vector3(positionBlock.x -0.03f,positionBlock.y, positionBlock.z);
                blockTransform.localPosition = block1LocalPos;
                block1.localPosition = block2LocalPos;
             
            }
            blockController.isHolded = true;
            if (blocksPickedUp == 4 ||  blocksPickedUp == 8) {
                    markBlock += 2;
                    positionBlock.z -= 0.1f;
                    positionBlock.y = 0f;
            }
            if (blocksPickedUp == 2 || blocksPickedUp == 6) {
                markBlock += 2;
                positionBlock.y += 0.1f;
            }   
            yield return null;
        }

    }
    public void Magnet () {
        GameObject[] blockObjects = GameObject.FindGameObjectsWithTag(blockTag);
        foreach (GameObject blockObject in blockObjects)
        {
            Transform blockTransform = blockObject.transform;
            float distance = Vector3.Distance(Player.position, blockTransform.position);
            if (distance <= distanceThreshold && !isWin)
            {
                StartCoroutine(holdingBlocks(blockTransform));
            }
        }
    }

    private void RealeaseBlock (List<Transform> pickedBlocks){
        if (isHoldingBlock && isTargeZone) {
             score += blocksPickedUp;
            foreach (Transform item in pickedBlocks)
            {
                var targetposition = new Vector3(Random.Range(TargetZone.position.x - 4.0f, TargetZone.position.x + 4.0f), TargetZone.position.y  + heightOfBlock/2f ,Random.Range(TargetZone.position.z -4.0f, TargetZone.position.z + 4.0f));
                item.transform.SetParent(TargetZone);
                item.transform.position = targetposition;
                DoneBlock.text = score + "/" + _doneBlocks;  
            }
            isHoldingBlock = false;
            pickedBlocks.Clear();
            blocksPickedUp = 0;
            markBlock = 0;
         }
         
             
    }


   
}
