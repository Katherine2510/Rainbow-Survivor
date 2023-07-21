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
    [SerializeField] private int _doneBlocks = 3;

    public Transform Player;
    public Transform TargetZone;

    private int _okBlocks = 0;

    public bool isWin = false;

    [SerializeField] private GameObject gameController;

    public Text DoneBlock;
    public float flySpeed = 5f;

    public List<Transform> pickedBlocks = new List<Transform>(); 
    

    Vector3 postionBlock = Vector3.zero;
    public string blockTag = "Block";
    public float distanceThreshold = 10f;

    public float heightOfBlock = 1f;
   
    

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
        Move();
        //thắng game + delay time + set position of player
        if (isWin)
        {
        Player.position = new Vector3( TargetZone.position.x, TargetZone.position.y + heightOfBlock/2f, TargetZone.position.z) ;
        // gameController.GetComponent<GameController>().StopCoroutine(StartCountDown);
        
        gameController.GetComponent<GameController>().StartDelayBeforeWin();
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


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Block"))
        {
            isHoldingBlock = true;
            Transform blockTransform = other.transform;
            // Kiểm tra xem block đã được nhặt chưa
             StartCoroutine(holdingBlocks(blockTransform));           
        }
        if (isHoldingBlock && other.CompareTag("TargetZone")) {
            postionBlock = Vector3.zero;
            isTargeZone = true;
            RealeaseBlock(pickedBlocks);          
        }
    }

    public IEnumerator  holdingBlocks (Transform blockTransform) {
        
        if (!pickedBlocks.Contains(blockTransform))
            {
                
                // Thêm block vào danh sách nhặt
                pickedBlocks.Add(blockTransform);
                // Đặt cha mẹ của block thành vị trí nhặt
                blockTransform.SetParent(holdPosition);
                while (blockTransform.localPosition != postionBlock )
                {
                    blockTransform.localPosition = Vector3.Lerp(blockTransform.localPosition, postionBlock, flySpeed *Time.deltaTime);
                    yield return null;
                }
                //blockTransform.localPosition = postionBlock;
                postionBlock.x += 0.05f;
             
            }
    }

    public void Magnet () {
        GameObject[] blockObjects = GameObject.FindGameObjectsWithTag(blockTag);
        // Kiểm tra khoảng cách từ mỗi đối tượng Block tới người chơi
        foreach (GameObject blockObject in blockObjects)
        {
            Transform blockTransform = blockObject.transform;
            float distance = Vector3.Distance(Player.position, blockTransform.position);
            // Kiểm tra nếu khoảng cách nhỏ hơn hoặc bằng distanceThreshold
            if (distance <= distanceThreshold && !isWin)
            {
                StartCoroutine(holdingBlocks(blockTransform));
            }
        }
    }

    private void RealeaseBlock (List<Transform> pickedBlocks){
         GameObject targetObject = GameObject.FindGameObjectWithTag("TargetZone");
      
         if (isHoldingBlock && isTargeZone) {
            foreach (Transform item in pickedBlocks)
            {
                var targetposition = new Vector3(Random.Range(targetObject.transform.position.x - 4.0f, targetObject.transform.position.x + 4.0f), targetObject.transform.position.y  + heightOfBlock/2f ,Random.Range(targetObject.transform.position.z -4.0f, targetObject.transform.position.z + 4.0f));
                item.transform.SetParent(null);
                item.transform.position = targetposition;  
                DoneBlock.text = pickedBlocks.Count + "/" + _doneBlocks;  
                isHoldingBlock = false;
              
            }
         }
         if (pickedBlocks.Count == _doneBlocks && !isWin && isTargeZone) {
                isWin = true;
                //Debug.Log(isWin);
            }
    }

   
}
