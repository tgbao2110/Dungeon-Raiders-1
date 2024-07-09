using UnityEngine;

public class BossRoom : EnemyRoom
{
    [SerializeField] private EventSystem eventSystem;
    private void Start()
    {
        dungeonGenerator = this.transform.parent.GetComponent<DungeonGenerator>();
        spawner = GetComponentInChildren<EnemySpawner>();
        eventSystem = FindObjectOfType<EventSystem>();
        this.SetNumberOfEnemies(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCleared)
        {
            InitializeRoom();
            if(eventSystem != null) 
            {
                eventSystem.ShowBossRoomIntro();
            }
        }
    }

}