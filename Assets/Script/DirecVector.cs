using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DirecVector : MonoBehaviour
{
    public string targetTag = "Block";
    public List<Transform> transformsList = new List<Transform>();
    public Transform Character;
    private NavMeshAgent agent;
    public Transform TargetZone;
    Vector3 targetZoneSize = new Vector3(10f, 10f, 10f);

    private void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in objectsWithTag)
        {
            Transform objTransform = obj.transform;
            transformsList.Add(objTransform);
        }
        agent = GetComponent<NavMeshAgent>();
        agent.transform.localPosition = Vector3.zero;
        agent.SetDestination(TargetZone.position); // Set initial destination to TargetZone
    }

    void Update()
    {
        bool blocksInRange = false;
        Transform nearestBlock = null;
        float nearestDistance = float.MaxValue;

        // Find the nearest unheld block
        foreach (Transform trans in transformsList)
        {
            BlockController blockController = trans.GetComponent<BlockController>();
            if (blockController != null && !blockController.isHolded)
            {
                blocksInRange = true;

                // Calculate the path to the current block
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(trans.position, path);
                
                // Calculate the total distance along the path
                float pathDistance = 0f;
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    pathDistance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                }

                if (pathDistance < nearestDistance)
                {
                    nearestDistance = pathDistance;
                    nearestBlock = trans;
                }
            }
        }

        if (blocksInRange)
        {
            agent.transform.localPosition = Vector3.zero;
            agent.SetDestination(nearestBlock.position);
        }
        else
        {
            agent.transform.localPosition = Vector3.zero;
            agent.SetDestination(TargetZone.position);
        }
    }
}
