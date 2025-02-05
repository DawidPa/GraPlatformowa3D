using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject pickupParticlesPrefab;

    public float powerUpDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateFireballPowerUp(fireballPrefab, powerUpDuration);
                if (pickupParticlesPrefab != null)
                {
                    Instantiate(pickupParticlesPrefab, transform.position, Quaternion.identity);
                }
                gameObject.SetActive(false); 
            }
            AudioManager.instance.PlaySFX(5);
        }
    }
   

}
