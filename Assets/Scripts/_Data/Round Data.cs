using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Scriptable Objects/Round Data")]
public class RoundData : ScriptableObject
{
    public int numOfEnemyRooms;
    public List<GameObject> itemsToDrop;
    //public List<int> numberOfEnemies;
    //public List<GameObject> enemiesToGenerate;
    public bool hasBossRoom;
}