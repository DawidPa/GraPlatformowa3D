using UnityEngine;
using UnityEngine.Animations;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float attackSpeed = 15f;
    public float chaseRange = 20f;
    public float attackRange = 5f;
    public float hoverHeight = 10f;
    public Animator anim;
    private Vector3 startPosition;
    private float chargeTimer;

    public enum AIState 
    { 
        isIdle, isChasing, isAttacking, isReturning 
    };

    private AIState currentState;

    void Start()
    {
        startPosition = transform.position;
        currentState = AIState.isIdle;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:
                anim.SetBool("IsMoving", false);
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }
                transform.LookAt(PlayerController.instance.transform, Vector3.up);

                break;

            case AIState.isChasing:
                //BUG FIX
                if (distanceToPlayer <= chaseRange)
                {
                    MoveTowards(new Vector3(target.position.x, target.position.y + hoverHeight, target.position.z), speed);
                    anim.SetBool("IsMoving", true);
                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                    if (distanceToPlayer <= attackRange)
                    {
                        currentState = AIState.isAttacking;
                        chargeTimer = 3f;
                        anim.SetTrigger("Attack");

                    }
                }
                else
                {
                    currentState = AIState.isReturning;
                }
                break;

            case AIState.isAttacking:
                if (distanceToPlayer <= attackRange)
                {
                    MoveTowards(target.position, attackSpeed);
                    chargeTimer -= Time.deltaTime;
                    if (chargeTimer <= 0f) currentState = AIState.isReturning;
                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                }
                else
                {
                    currentState = AIState.isReturning;
                }
                break;

            case AIState.isReturning:
                if (distanceToPlayer >= attackRange || distanceToPlayer >= chaseRange)
                { 
                MoveTowards(startPosition, speed);
                    if (Vector3.Distance(transform.position, startPosition) < 0.5f)
                    {
                        currentState = AIState.isIdle;
                    }
                transform.LookAt(startPosition, Vector3.up);
                }
                break;
        }
        
    }
    //"Set Destination"
    private void MoveTowards(Vector3 targetPosition, float moveSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

}
