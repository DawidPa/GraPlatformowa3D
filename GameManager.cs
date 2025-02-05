using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform spawnPoint;

    private List<PowerUp> allPowerUps = new List<PowerUp>();
    private List<HealthPickup> allHealthPickups = new List<HealthPickup>();
    private List<EnemyHealth> allEnemies = new List<EnemyHealth>();
   

    public int coinCount = 0;

    private void Awake()
    {
        instance = this;
        allPowerUps.AddRange(FindObjectsOfType<PowerUp>());
        allHealthPickups.AddRange(FindObjectsOfType<HealthPickup>());
        allEnemies.AddRange(FindObjectsOfType<EnemyHealth>());    
    }

    private void Start()
    {

        RespawnPlayer();
        HideCursor();
        AudioManager.instance.PlayMusic(0);

        //Test
        StartCoroutine(SpawnPowerUps());
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetHealthPickups()
    {
        foreach (HealthPickup health in allHealthPickups)
        {
            if (health != null)
                health.gameObject.SetActive(true);
        }
    }
    public void ResetPowerUps()
    {
        foreach (PowerUp powerUp in allPowerUps)
        {
            if (powerUp != null)
                powerUp.gameObject.SetActive(true);
        }
    }

    public void ResetEnemies()
    {
        foreach (EnemyHealth enemy in allEnemies)
        {
            if (enemy != null)
                enemy.ResetEnemy();
        }
    }

    public void SetCheckpoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null || UIManager.instance == null) return;

        UIManager.instance.ShowBlackScreen();
        StartCoroutine(RespawnWithDelay(player));
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true) 
        {
            yield return new WaitForSeconds(15f);

            ResetPowerUps(); ; 
        }
    }
    private IEnumerator RespawnWithDelay(GameObject player)
    {
        player.SetActive(false);

        yield return new WaitForSeconds(2f);

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        if (controller != null)
        {
            controller.enabled = true;
        }
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth?.ResetHealth();

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController?.DeactivateFireballPowerUp();
        playerController?.ResetKnockback();

        ResetHealthPickups();
        ResetPowerUps();
        ResetEnemies();

        player.SetActive(true);


        StartCoroutine(UIManager.instance.FadeOutBlackScreen());
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UIManager.instance.UpdateCoinText(coinCount);
    }
}

