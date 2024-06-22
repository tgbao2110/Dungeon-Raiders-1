using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject baseRoomPrefab;
    public GameObject enemyRoomPrefab;
    public GameObject rewardRoomPrefab;
    public GameObject hallwayPrefab;
    public int numOfEnemyRooms = 3;
    public int numOfRewardRooms = 0;
    public int gridWidth = 10;
    public int gridHeight = 10;

    private Dictionary<Vector2Int, GameObject> roomGrid = new Dictionary<Vector2Int, GameObject>();
    private List<Vector2Int> availablePositions = new List<Vector2Int>();
    private Vector2Int currentRoom;
    private Vector2Int previousRoom;
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
        PlaceRoom(baseRoomPrefab, basePosition);
        currentRoom = basePosition;
        allRooms.Add(currentRoom);

        for (int i = 0; i < numOfEnemyRooms + numOfRewardRooms; i++)
        {
            GameObject roomPrefab = i < numOfEnemyRooms ? enemyRoomPrefab : rewardRoomPrefab;
            PlaceNextRoom(roomPrefab);
        }

        //ConnectRooms();
    }

    void PlaceRoom(GameObject roomPrefab, Vector2Int position)
    {
        if (!roomGrid.ContainsKey(position))
        {
            GameObject room = Instantiate(roomPrefab, new Vector3(position.x * 10, position.y * 10, 0), Quaternion.identity);
            room.transform.parent = this.transform;
            roomGrid[position] = room;
            availablePositions.Remove(position);
        }
    }

    void PlaceNextRoom(GameObject roomPrefab)
    {
        List<Vector2Int> possiblePositions = new List<Vector2Int>
        {
            new Vector2Int(currentRoom.x + 1, currentRoom.y),
            new Vector2Int(currentRoom.x - 1, currentRoom.y),
            new Vector2Int(currentRoom.x, currentRoom.y + 1),
            new Vector2Int(currentRoom.x, currentRoom.y - 1)
        };

        possiblePositions.RemoveAll(pos => !availablePositions.Contains(pos) || allRooms.Contains(pos));

        if (possiblePositions.Count == 0) return; // No available position

        previousRoom = currentRoom;
        currentRoom = possiblePositions[Random.Range(0, possiblePositions.Count)];
        PlaceRoom(roomPrefab, currentRoom);
        allRooms.Add(currentRoom);
    }

    void ConnectRooms()
    {
        foreach (var room in roomGrid)
        {
            List<Vector2Int> possibleConnections = new List<Vector2Int>
            {
                new Vector2Int(room.Key.x + 1, room.Key.y),
                new Vector2Int(room.Key.x - 1, room.Key.y),
                new Vector2Int(room.Key.x, room.Key.y + 1),
                new Vector2Int(room.Key.x, room.Key.y - 1)
            };

            foreach (Vector2Int connection in possibleConnections)
            {
                if (roomGrid.ContainsKey(connection))
                {
                    if ((room.Key == previousRoom && connection == currentRoom) || (room.Key == currentRoom && connection == previousRoom))
                    {
                        GameObject roomA = roomGrid[room.Key];
                        GameObject roomB = roomGrid[connection];
                        Vector3 hallwayPosition = (roomA.transform.position + roomB.transform.position) / 2;
                        Instantiate(hallwayPrefab, hallwayPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}
