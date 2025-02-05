using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f; 
    public int damageAmount = 1; 

    private void Start()
    {
  
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerHealth != null && playerController != null)
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                knockbackDirection.y = 0f;

                playerHealth.TakeDamage(damageAmount);
                playerController.ApplyKnockback(knockbackDirection);

                Debug.Log("Player took damage by projectile");
                AudioManager.instance.PlaySFX(1);
            }
            Destroy(gameObject);
        }
       
    }
}
