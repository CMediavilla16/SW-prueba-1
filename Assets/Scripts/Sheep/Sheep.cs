using UnityEngine;
using UnityEngine.AI;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Transform targetTransform;

    NavMeshAgent navMeshAgent;

    Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

    }



    void Update()
    {
        navMeshAgent.SetDestination(targetTransform.position);

        AdjustAnimationsAndRotation();
    }


    public void AdjustAnimationsAndRotation()
    {
        bool isMoving = navMeshAgent.velocity.sqrMagnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);

        if (navMeshAgent.desiredVelocity.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (navMeshAgent.desiredVelocity.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
}
