using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
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
            GameManager.instance.AddCoins(coinValue);          
            if (pickupParticlesPrefab != null)
            {
                Instantiate(pickupParticlesPrefab, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);  
            Debug.Log("Player picked up coin");
            AudioManager.instance.PlaySFX(3);
        }
    }
    public void ResetPickup()
    {
        transform.position = initialPosition; 
        gameObject.SetActive(true);          
    }
}
