using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemyController : MonoBehaviour
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
    public float attackRange = 5f; 

    public float projectileSpeed = 10f;
    public float timeBetweenShots = 2f;

    private float shootCounter;

    public GameObject projectilePrefab;
    public Transform shootPoint;  

  
    void Start()
    {
        waitCounter = waitAtPoint;
        shootCounter = timeBetweenShots;
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

                if (distanceToPlayer > attackRange)
                {
                    agent.isStopped = false;
                    agent.SetDestination(PlayerController.instance.transform.position);
                }
                else
                {
                    agent.isStopped = true;
                    anim.SetBool("IsMoving", false);

                    currentState = AIState.isAttacking;
                    shootCounter = 0; 
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
                anim.SetBool("IsMoving", false);
                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                shootCounter -= Time.deltaTime;

                if (shootCounter <= 0)
                {
                    anim.SetTrigger("Attack"); 
                    shootCounter = timeBetweenShots;
                }

                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.isChasing;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                    agent.SetDestination(transform.position);
                }

                break;

        }
    }

    public void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 targetPosition = PlayerController.instance.transform.position;
                targetPosition.y += 1; 

                Vector3 direction = (targetPosition - shootPoint.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
        }
    }

}
