
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    private Transform player;
    private MeshRenderer box;
    [SerializeField] private float wanderRadius = 1000f;
    [SerializeField] private float wanderTimer = 3f;
    [SerializeField] private float stoppingDistance = 1f;
    private GameController gameController;
    private GameObject characterMovement;
    private EnemyAnimation animatorEnemy;
    private NavMeshAgent agent;
    private Transform enemy;
    private float timer;
    private Vector3 previousPosition;
    private void OnEnable()
    {
        box = GameObject.FindGameObjectWithTag("Body").GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Transform>();
        previousPosition = box.transform.position;
        timer = wanderTimer;

        // Find and set references to player, game controller, character movement, and enemy animation
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        gameController = GameObject.Find("GameController")?.GetComponent<GameController>();
        characterMovement = GameObject.FindGameObjectWithTag("Character");

        // Ensure the EnemyAnimation component exists in the GameObject or its children
        animatorEnemy = GetComponentInChildren<EnemyAnimation>();

        animatorEnemy.PlayRun();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        Transform nearestTarget = GetNearestTarget();
        float distance = Vector3.Distance(transform.position, nearestTarget.position);
        if (nearestTarget != null && distance < 10f)
        {
            if (nearestTarget.CompareTag("bot") || (nearestTarget.CompareTag("Player") && (!box.enabled || box.transform.position != previousPosition)))
            agent.SetDestination(nearestTarget.position);
        }
        bool isWithinStoppingDistance = distance < stoppingDistance + 2f ;
        if (isWithinStoppingDistance)
        {
            if (nearestTarget.CompareTag("bot"))
            {
                animatorEnemy.PlayCatch();
                Transform block = nearestTarget.Find("Block(Clone)");
                if (block != null)
                {
                    BlockController blockController = block.GetComponent<BlockController>();
                    block.SetParent(null);
                    Vector3 dropBlock = transform.position;
                    dropBlock.y += 1;
                    block.position = dropBlock;
                    blockController.isHolded = false;
                    nearestTarget.gameObject.SetActive(false);
                } else {
                    nearestTarget.gameObject.SetActive(false);
                }
   
            }
            else if (nearestTarget.CompareTag("Player"))
            {
                //GameManagement.Instance.DelayedFunction();
                animatorEnemy.PlayCatch();
                Debug.Log("Thua");
                gameController.AreCatched();
            }
        }

        bool playerIsNear = distance < 10f && (!box.enabled || box.transform.position != previousPosition);
        if (!isWithinStoppingDistance && !playerIsNear && timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0f;
        }

        previousPosition = box.transform.position;
    }
    private Transform GetNearestTarget()
    {
        GameObject[] botObjects = GameObject.FindGameObjectsWithTag("bot");
        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject bot in botObjects)
        {
            float distanceToBot = Vector3.Distance(transform.position, bot.transform.position);
            if (distanceToBot < nearestDistance && distanceToBot < 10f)
            {
                nearestTarget = bot.transform;
                nearestDistance = distanceToBot;
            }
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < nearestDistance)
        {
            nearestTarget = player;
        }

        return nearestTarget;
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
