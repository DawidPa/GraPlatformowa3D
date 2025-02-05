using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                AudioManager.instance.PlaySFX(8);
            }
            Destroy(gameObject);

        }
        

    }
}
