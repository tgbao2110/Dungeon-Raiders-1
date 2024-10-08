using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DungeonGenerator : MonoBehaviour
{

    private LevelData levelData;
    private RoundData roundData;
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 10;

    [Header("Rewards")]
    [SerializeField] protected GameObject chestPrefab;
    private int currentItemIndex = 0;
    private Dictionary<Vector2Int, GameObject> roomGrid = new Dictionary<Vector2Int, GameObject>();
    private List<Vector2Int> availablePositions = new List<Vector2Int>();
    private GameObject previousRoom;
    private Vector2Int currentRoomGridPos;
    private Vector2Int previousRoomGridPos;
    private List<Vector2Int> allRooms = new List<Vector2Int>();


    public void StartGame(LevelData levelData, RoundData roundData)
    {
        this.levelData = levelData;
        this.roundData = roundData;
        InitializeGridPositions();
        GenerateRooms();
        MiniMap();
    }

    void InitializeGridPositions()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                availablePositions.Add(new Vector2Int(x, y));
            }
        }
    }

    void GenerateRooms()
    {
        // Create Base Room
        Vector2Int basePosition = new Vector2Int(0, gridHeight / 2);
        PlaceRoom(levelData.baseRoomPrefab, basePosition, new Vector3(0, 0, 0), 0);
        currentRoomGridPos = basePosition;
        allRooms.Add(currentRoomGridPos);

        // Create Enemy Rooms
        for (int i = 0; i < roundData.numOfEnemyRooms; i++)
        {
            GameObject roomPrefab = levelData.enemyRoomPrefab;
            PlaceNextRoom(roomPrefab, roundData.numberOfEnemies[i]);
        }

        // Create Boss Room if allowed
        if (roundData.hasBossRoom)
        {
            PlaceNextRoom(levelData.bossRoomPrefab, 0);
        }

        PlaceNextRoom(levelData.portalRoomPrefab, 0);
    }

    void PlaceNextRoom(GameObject roomPrefab, int numberOfEnemies)
    {
        List<Vector2Int> possiblePositions = new List<Vector2Int>
    {
        new Vector2Int(currentRoomGridPos.x + 1, currentRoomGridPos.y),
        new Vector2Int(currentRoomGridPos.x - 1, currentRoomGridPos.y),
        new Vector2Int(currentRoomGridPos.x, currentRoomGridPos.y + 1),
        new Vector2Int(currentRoomGridPos.x, currentRoomGridPos.y - 1)
    };

        possiblePositions.RemoveAll(pos => !availablePositions.Contains(pos) || allRooms.Contains(pos));

        if (possiblePositions.Count == 0) return; // No available position

        previousRoomGridPos = currentRoomGridPos;
        currentRoomGridPos = possiblePositions[Random.Range(0, possiblePositions.Count)];

        Vector3 previousRoomPosition = previousRoom.transform.localPosition;
        Vector3 newRoomPosition = Vector3.zero;

        switch (currentRoomGridPos)
        {
            case var pos when pos == new Vector2Int(previousRoomGridPos.x + 1, previousRoomGridPos.y):
                newRoomPosition = previousRoomPosition + new Vector3(20, 0, 0);
                break;
            case var pos when pos == new Vector2Int(previousRoomGridPos.x - 1, previousRoomGridPos.y):
                newRoomPosition = previousRoomPosition + new Vector3(-20, 0, 0);
                break;
            case var pos when pos == new Vector2Int(previousRoomGridPos.x, previousRoomGridPos.y + 1):
                newRoomPosition = previousRoomPosition + new Vector3(0, 20, 0);
                break;
            case var pos when pos == new Vector2Int(previousRoomGridPos.x, previousRoomGridPos.y - 1):
                newRoomPosition = previousRoomPosition + new Vector3(0, -20, 0);
                break;
        }

        PlaceRoom(roomPrefab, currentRoomGridPos, newRoomPosition, numberOfEnemies);
        ConnectRooms(previousRoomGridPos, currentRoomGridPos);
        allRooms.Add(currentRoomGridPos);
    }

    void PlaceRoom(GameObject roomPrefab, Vector2Int gridPosition, Vector3 position, int numberOfEnemies)
    {
        if (!roomGrid.ContainsKey(gridPosition))
        {
            previousRoom = Instantiate(roomPrefab, position, Quaternion.identity);
            previousRoom.transform.parent = this.transform;
            roomGrid[gridPosition] = previousRoom;
            availablePositions.Remove(gridPosition);

            EnemyRoom enemyRoom = previousRoom.GetComponent<EnemyRoom>();
            if (enemyRoom != null)
            {
                enemyRoom.SetNumberOfEnemies(numberOfEnemies);
            }
        }
    }



    void ConnectRooms(Vector2Int roomA, Vector2Int roomB)
    {
        Vector3 hallwayPosition = (roomGrid[roomA].transform.position + roomGrid[roomB].transform.position) / 2;
        Vector3 hallwayRotation = Vector3.zero;
        GameObject hallway;
        if (roomA.x != roomB.x)
        {
            hallway = Instantiate(levelData.hallwayHorizontalPrefab, hallwayPosition, Quaternion.Euler(hallwayRotation));
            hallway.transform.parent = this.transform;
            if (roomA.x > roomB.x) hallway.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            if (roomA.y < roomB.y)
            {
                hallway = Instantiate(levelData.hallwayVerticalUpPrefab, hallwayPosition, Quaternion.Euler(hallwayRotation));
            }
            else
            {
                hallway = Instantiate(levelData.hallwayVerticalDownPrefab, hallwayPosition, Quaternion.Euler(hallwayRotation));
            }
            hallway.transform.parent = this.transform;

        }

        Hallway hallwayComponent = hallway.GetComponent<Hallway>();
        roomGrid[roomA].GetComponent<Room>().SetToHallway(hallwayComponent);
        roomGrid[roomB].GetComponent<Room>().SetFromHallway(hallwayComponent);
    }

    public void SpawnChest(Vector3 position)
    {
        GameObject chest = Instantiate(chestPrefab, position, Quaternion.identity);
        var chestComponent = chest.GetComponent<RewardChest>();
        chestComponent.InitializeChest(roundData.itemsToDrop[currentItemIndex]);
        currentItemIndex++;
        if (currentItemIndex >= roundData.itemsToDrop.Count)
        {
            currentItemIndex = 0;
        }
    }

    //DEBUG
    void MiniMap()
    {
        // Create a 2D array to represent the grid
        string[,] grid = new string[gridWidth, gridHeight];

        // Initialize the grid with empty spaces
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = " ";
            }
        }

        // Fill the grid with room types
        foreach (var kvp in roomGrid)
        {
            Vector2Int gridPosition = kvp.Key;
            GameObject room = kvp.Value;
            string roomType = room.name.Replace("(Clone)", "").Trim();
            grid[gridPosition.x, gridPosition.y] = roomType.Substring(0, 1); // Use the first letter to represent the room type
        }

        // Log the grid in matrix format
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                row += "[" + grid[x, y] + "]";
            }
            Debug.Log(row);
        }
    }
}
