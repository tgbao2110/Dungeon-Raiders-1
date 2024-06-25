using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject baseRoomPrefab;
    public GameObject enemyRoomPrefab;
    public GameObject rewardRoomPrefab;
    public GameObject hallwayHorizontalPrefab;
    public GameObject hallwayVerticalPrefab;
    public int numOfEnemyRooms = 3;
    public int numOfRewardRooms = 0;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public bool isLocked = false;

    private Dictionary<Vector2Int, GameObject> roomGrid = new Dictionary<Vector2Int, GameObject>();
    private List<Vector2Int> availablePositions = new List<Vector2Int>();
    private GameObject previousRoom;
    private Vector2Int currentRoomGridPos;
    private Vector2Int previousRoomGridPos;
    private List<Vector2Int> allRooms = new List<Vector2Int>();

    void Start()
    {
        InitializeGridPositions();
        GenerateRooms();
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
        Vector2Int basePosition = new Vector2Int(0, gridHeight / 2);
        PlaceRoom(baseRoomPrefab, basePosition, new (0,0,0));
        currentRoomGridPos = basePosition;
        allRooms.Add(currentRoomGridPos);

        for (int i = 0; i < numOfEnemyRooms + numOfRewardRooms; i++)
        {
            GameObject roomPrefab = i < numOfEnemyRooms ? enemyRoomPrefab : rewardRoomPrefab;
            PlaceNextRoom(roomPrefab);
        }
    }

    void PlaceRoom(GameObject roomPrefab, Vector2Int gridPosition, Vector3 position)
    {
        if (!roomGrid.ContainsKey(gridPosition))
        {
            previousRoom = Instantiate(roomPrefab, position, Quaternion.identity);
            previousRoom.transform.parent = this.transform;
            roomGrid[gridPosition] = previousRoom;
            availablePositions.Remove(gridPosition);
        }
    }

    void PlaceNextRoom(GameObject roomPrefab)
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
                newRoomPosition = previousRoomPosition + new Vector3(20,0,0);
                break;
            case var pos when pos == new Vector2Int(previousRoomGridPos.x - 1, previousRoomGridPos.y):
                newRoomPosition = previousRoomPosition + new Vector3(-20,0,0);
                break;
            case var pos when pos == new Vector2Int(previousRoomGridPos.x, previousRoomGridPos.y + 1):
                newRoomPosition = previousRoomPosition + new Vector3(0,20,0);
                break;
            case var pos when pos == new Vector2Int(previousRoomGridPos.x, previousRoomGridPos.y - 1):
                newRoomPosition = previousRoomPosition + new Vector3(0,-20,0);
                break;
        }

        PlaceRoom(roomPrefab, currentRoomGridPos, newRoomPosition);
        ConnectRooms(previousRoomGridPos, currentRoomGridPos);
        allRooms.Add(currentRoomGridPos);
    }

    void ConnectRooms(Vector2Int roomA, Vector2Int roomB)
    {
        Vector3 hallwayPosition = (roomGrid[roomA].transform.position + roomGrid[roomB].transform.position) / 2;

        // Determine hallway orientation
        Vector3 hallwayRotation = Vector3.zero;
        GameObject hallway;

        if (roomA.x != roomB.x)
        {
            hallway = Instantiate(hallwayHorizontalPrefab, hallwayPosition, Quaternion.Euler(hallwayRotation));
            hallway.transform.parent = this.transform;
        }
        else
        {
            hallway = Instantiate(hallwayVerticalPrefab, hallwayPosition, Quaternion.Euler(hallwayRotation));
            hallway.transform.parent = this.transform;
        }
    }
}
