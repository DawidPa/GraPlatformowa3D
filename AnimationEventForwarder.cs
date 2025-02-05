using UnityEngine;

public class AnimationEventForwarder : MonoBehaviour
{
    public ShootingEnemyController shootingEnemyController; 

    public void ShootProjectile()
    {
        if (shootingEnemyController != null)
        {
            shootingEnemyController.ShootProjectile(); 
        }
    }
}
