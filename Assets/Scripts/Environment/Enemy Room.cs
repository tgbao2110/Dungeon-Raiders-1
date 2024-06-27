using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public int numberOfEnemies;
    public int enemiesCount;
    PlayerController playerController;

    private void Awake() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            playerController.SetRoom(this);
            enemiesCount = numberOfEnemies;
            Lock();
        }
    }

    protected void Lock()
    {
        
    }
}
