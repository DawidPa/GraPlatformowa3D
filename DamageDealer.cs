using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 1;
    public float damageInterval = 1f;
    private float nextDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                PlayerController playerController = other.GetComponent<PlayerController>();

                if (playerHealth != null && playerController != null)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;  
                    playerHealth.TakeDamage(damageAmount);
                    playerController.ApplyKnockback(knockbackDirection);
                    Debug.Log($"Player took damage");
                    nextDamageTime = Time.time + damageInterval;
                    AudioManager.instance.PlaySFX(1);

                }
            }
        }
    }
}
