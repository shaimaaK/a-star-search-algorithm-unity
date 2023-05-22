using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePosition;
    NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        navMeshAgent.destination=movePosition.position;
    }
}
