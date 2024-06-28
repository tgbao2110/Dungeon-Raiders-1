using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hallway : MonoBehaviour
{
    [SerializeField] List<Door> doors;

    public void SetDoorsLocked(bool isLocked)
    {
        foreach (Door door in doors)
        {
            door.SetDoorLocked(isLocked);
        }

        var tilemap = this.GetComponent<TilemapRenderer>();
        tilemap.sortingOrder = isLocked ? 0 : 2;
    }
}
