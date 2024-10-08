using System;
using TMPro;
using UnityEngine;

public class EnemyRoom : Room
{
    public int numberOfEnemies;
    public int enemiesCount;
    protected bool isCleared = false;
    protected PlayerController playerController;
    [SerializeField] protected EnemySpawner spawner;

    private void Start()
    {
        dungeonGenerator = this.transform.parent.GetComponent<DungeonGenerator>();
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

        enemiesCount = numberOfEnemies;
        Lock();
        spawner.Spawn(numberOfEnemies);
    }

    public void SetNumberOfEnemies(int numEnemies)
    {
        numberOfEnemies = numEnemies;
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
        enemiesCount -= 1;
        if (enemiesCount <= 0)
        {
            isCleared = true;
            Actions.OnEnemyRoomCleared.Invoke();
            Unlock();
            dungeonGenerator.SpawnChest(this.transform.position);
        }
    }


}
