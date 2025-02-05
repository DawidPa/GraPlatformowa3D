using UnityEngine;

public class Killer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag("Player"))
        {         
            GameManager.instance.RespawnPlayer();
        }
    }









}
