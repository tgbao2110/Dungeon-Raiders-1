using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<RoundData> rounds;
    public GameObject baseRoomPrefab;
    public GameObject enemyRoomPrefab;
    public GameObject bossRoomPrefab;
    public GameObject portalRoomPrefab;
    public GameObject hallwayHorizontalPrefab;
    public GameObject hallwayVerticalUpPrefab;
    public GameObject hallwayVerticalDownPrefab;
}
