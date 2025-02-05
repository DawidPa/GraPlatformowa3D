using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    public Animator anim;

    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking
    };

    public AIState currentState;
    public float waitAtPoint = 2f;
    private float waitCounter;
    public float chaseRange;
    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    
    void Start()
    {
        waitCounter = waitAtPoint;
        currentState = AIState.isIdle;
    }

  
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (currentState)
        {
            case AIState.isIdle:
                anim.SetBool("IsMoving", false);

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.isPatrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                break;

            case AIState.isPatrolling:
                anim.SetBool("IsMoving", true);

                if (agent.remainingDistance <= 0.2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                break;

            case AIState.isChasing:
                anim.SetBool("IsMoving", true);

                agent.isStopped = false;
                agent.SetDestination(PlayerController.instance.transform.position);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.isAttacking;

                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                    attackCounter = 0; 
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;

                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                break;

            case AIState.isAttacking:
                agent.velocity = Vector3.zero; // BUG FIX
                agent.isStopped = true;

                anim.SetBool("IsMoving", false);

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;

                if (attackCounter <= 0)
                {
                    if (distanceToPlayer <= attackRange)
                    {
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks; 
                    }
                    else
                    {
                        currentState = AIState.isChasing; 
                        agent.isStopped = false;
                    }
                }
                break;
        }
    }
}
