using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    private float wanderRadius = 1000f;
    private float wanderTimer = 3;
    [SerializeField] private Transform target;
    [SerializeField] private MeshRenderer Box;
    private NavMeshAgent agent;
    private Transform enemy;
    private float timer;
    public float stoppingDistance = 1f;
    [SerializeField] private GameObject gameController;
    private Vector3 previousPosition;
    [SerializeField] private GameObject _characterMovement;
    public GameObject animatorEnemy;

    void OnEnable () {

        agent = GetComponent<NavMeshAgent> ();
        enemy = GetComponent<Transform> ();
        previousPosition = Box.transform.position;
        timer = wanderTimer;
        animatorEnemy.GetComponent<EnemyAnimation>().PlayRun();
    }
 
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        Vector3 lengthBetween = enemy.position - target.position;
        if (lengthBetween.sqrMagnitude < 100 && (!Box.enabled || Box.transform.position != previousPosition)) {
            //Debug.Log("Bắt đầu đuổi");
            agent.SetDestination(target.transform.position);
        }
        else if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        float distance = Vector3.Distance(transform.position, target.position);
        if (Box.enabled && Box.transform.position == previousPosition){
            //Xử kiện né đi hướng khác
        }
        else if (distance <= stoppingDistance && !_characterMovement.GetComponent<CharacterMovement>().isWin)
        {   
             gameController.GetComponent<GameController>().AreCatched();
        }
        
         if (lengthBetween.sqrMagnitude < 9 && (!Box.enabled || Box.transform.position != previousPosition)) {
             animatorEnemy.GetComponent<EnemyAnimation>().PlayCatch();
        }
        previousPosition = Box.transform.position;
    }
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }

    
    
}
