using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;  
    public GameObject pickupParticlesPrefab; 
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    playerHealth.TakeHeal(healAmount);

                    if (pickupParticlesPrefab != null)
                    {
                        Instantiate(pickupParticlesPrefab, transform.position, Quaternion.identity);
                    }

                    gameObject.SetActive(false); 
                    Debug.Log("Player picked up HP");
                    AudioManager.instance.PlaySFX(4);
                }
                else
                {
                    Debug.Log("Player alredy full health");
                }
            }
        }
    }

    public void ResetPickup()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);       
    }
}
