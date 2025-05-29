using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTester : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
}
