using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damage = 1; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamagePoint"))
        {

            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                AudioManager.instance.PlaySFX(2);

            }
        }
    }
}
