using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 10f;
    public float rotationSpeed = 10f;
    public float gravityScale = 5f;
    private Vector3 moveDirection;
    public bool isGrounded;
    private float yVelocity;
    private bool isSprinting = false;
    private bool wasGroundedLastFrame = true;
    private bool isJumping = false;
    private float jumpCooldownTimer = 0f;
    public float bounceForce = 8f;

    public Vector2 knockbackForce;
    public float knockbackDuration = 0.5f;
    private float knockbackTimer;
    public bool isKnockedBack = false;
    private Vector3 knockbackDirection;

    public Animator anim;
   
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    private bool canShootFireballs = false;
    private float fireballCooldown = 0.5f;
    private float lastFireballTime = 0f;

    // Komponenty
    public CharacterController characterController;
    public Transform cameraTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (isKnockedBack)
        {
            HandleKnockback();
            return;
        }

        isGrounded = characterController.isGrounded;

        Movement();
        Jump();

        if (Time.timeScale > 0f)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Running", isSprinting);

        wasGroundedLastFrame = isGrounded;

        if (jumpCooldownTimer > 0f)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        if (canShootFireballs && Input.GetKeyDown(KeyCode.F) && Time.time > lastFireballTime + fireballCooldown)
        {
            ShootFireball();
            lastFireballTime = Time.time;
            AudioManager.instance.PlaySFX(6);
        }
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        isSprinting = sprint && (moveHorizontal != 0 || moveVertical != 0);

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * moveVertical + right * moveHorizontal;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        moveDirection = move * moveSpeed * (isSprinting ? sprintMultiplier : 1f);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            if (!wasGroundedLastFrame)
            {
                isJumping = false;
            }

            if (!isJumping)
            {
                yVelocity = -1f;
            }

            if (Input.GetButtonDown("Jump") && !isJumping && jumpCooldownTimer <= 0f)
            {
                isJumping = true;
                yVelocity = jumpForce;
                anim.SetBool("IsJumping", true);
                jumpCooldownTimer = 0.1f;
                AudioManager.instance.PlaySFX(0);
            }
        }
        else
        {
            anim.SetBool("IsJumping", isJumping);
        }

        yVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        moveDirection.y = yVelocity;
    }

    public void ApplyKnockback(Vector3 direction)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        knockbackDirection = direction * knockbackForce.x;
        knockbackDirection.y = knockbackForce.y;
    }

    private void HandleKnockback()
    {
        knockbackTimer -= Time.deltaTime;

        characterController.Move(knockbackDirection * Time.deltaTime);

        if (knockbackTimer <= 0)
        {
            isKnockedBack = false;
        }
    }

    public void ResetKnockback()
    {
        isKnockedBack = false;
        moveDirection = Vector3.zero;
    }


    public void Bounce()
    {
        yVelocity = bounceForce;
        characterController.Move(moveDirection * Time.deltaTime);
      
    }

    public void ActivateFireballPowerUp(GameObject prefab, float duration)
    {
        fireballPrefab = prefab;
        canShootFireballs = true;
        Invoke(nameof(DeactivateFireballPowerUp), duration);
    }

    public void DeactivateFireballPowerUp()
    {
        canShootFireballs = false;
    }

    private void ShootFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 shootDirection = cameraTransform.forward;
                shootDirection.Normalize();
                rb.velocity = shootDirection * 10f;
            }
        }
    }



}




