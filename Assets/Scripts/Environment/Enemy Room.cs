using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRoom : Room
{
    public int numberOfEnemies;
    public int enemiesCount;
    private bool isCleared = false;
    private PlayerController playerController;
    [SerializeField] private EnemySpawner spawner;

    private void Start() {
        spawner = GetComponentInChildren<EnemySpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCleared)
        {
            InitializeRoom();
        }
    }

    public override void InitializeRoom()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        playerController.SetRoom(this);

        numberOfEnemies = spawner.GetLength();
        enemiesCount = numberOfEnemies;
        Lock();
        spawner.Spawn();
    }

    protected void Lock()
    {
        if (fromHallway != null)
        { fromHallway.SetDoorsLocked(true); }
        if (toHallway != null)
        { toHallway.SetDoorsLocked(true); }
    }

    public void Unlock()
    {
        if (fromHallway != null)
        { fromHallway.SetDoorsLocked(false); }
        if (toHallway != null)
        { toHallway.SetDoorsLocked(false); }
    }

    public void KillEnemy()
    {
        enemiesCount--;
        if (enemiesCount <= 0)
        {
            isCleared=true;
            Unlock();
        }
    }
}
