using UnityEngine;

public class CameraLayerCulling : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float maxDistance = 10f;
    public LayerMask wallLayerMask;
    private Camera mainCamera;
    public Transform follow;
    public Vector3 previousPosition;

    private void Start()
    {
        mainCamera = Camera.main;
        previousPosition = follow.transform.position;

    }
    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference not set!");
            return;
        }
        Vector3 directionToPlayer = player.position - mainCamera.transform.position;
        Ray ray = new Ray(mainCamera.transform.position, directionToPlayer);
        RaycastHit hit;
        bool hasWallBetweenCameraAndPlayer = Physics.Raycast(ray, out hit, maxDistance, wallLayerMask);
        while (hasWallBetweenCameraAndPlayer)
        {
            Debug.Log("Có Wall ở giữa");
            Vector3 desiredCameraPosition = player.position - directionToPlayer.normalized * maxDistance;
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredCameraPosition, followSpeed * Time.deltaTime);
            follow.transform.position = Vector3.MoveTowards(follow.transform.position, player.transform.position, followSpeed * Time.deltaTime);
            return;
        }    
        //follow.transform.position = previousPosition;
    }
}
