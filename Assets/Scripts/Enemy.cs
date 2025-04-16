using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    [Header("Setting")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManagerState();
    }

    private void ManagerState()
    {
        switch(state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunnerTowardsTarget();
                break;

        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);
        for(int i = 0; i < detectedColliders.Length; i++)
        {
            if(detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTaget())
                    continue;

                runner.SetTaget();
                targetRunner = runner.transform;
                StartRunningTowardsTaget();
            }
        }
    }

    private void StartRunningTowardsTaget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunnerTowardsTarget()
    {
        if(targetRunner == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position,targetRunner.position, Time.deltaTime*moveSpeed );
        if(Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
