using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class pitchTiger : MonoBehaviour
{
    public Transform[] moveTargets;

    private NavMeshAgent navMeshAgent;

    public int indexOfTarget = 0;

    private Rigidbody rb;

    public float speed = 3.5f;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Init() 
    {
        StartCoroutine(Move());
    }

    private void LateUpdate()
    {
        navMeshAgent.speed = speed;
    }

    private IEnumerator Move() 
    {
        navMeshAgent.SetDestination(moveTargets[indexOfTarget].position);
        while (transform.position.x != moveTargets[indexOfTarget].position.x)
        {
            yield return null;
        }
        indexOfTarget+= 1;
        if (indexOfTarget >= moveTargets.Length)
        {
            indexOfTarget = 0;
        }
        StartCoroutine(Move());
    }
}
