using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointON, checkpointOFF;
    public Transform respawnPoint;
    private static List<Checkpoint> allCheckpoints;

    private void Awake()
    {
     
        if (allCheckpoints == null)
        {
            allCheckpoints = new List<Checkpoint>();
        }

        allCheckpoints.Add(this);
    }

    private void OnDestroy()
    {
        allCheckpoints.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("FireBall"))
        {
            if (checkpointON.activeSelf)
            {
                Debug.Log("Checkpoint already active");
                return;
            }
            ActivateCheckpoint();
            Debug.Log("Checkpoint set");
            AudioManager.instance.PlaySFX(9);

        }
    }
    private void ActivateCheckpoint()
    {
        foreach (Checkpoint checkpoint in allCheckpoints)
        {
            if (checkpoint == this)
            {
                checkpoint.SetActiveCheckpoint(true);
                GameManager.instance.SetCheckpoint(respawnPoint);
            }
            else
            {
                checkpoint.SetActiveCheckpoint(false); 
            }
        }
    }
    public void SetActiveCheckpoint(bool isActive)
    {
        checkpointON.SetActive(isActive);
        checkpointOFF.SetActive(!isActive);
    }
}
