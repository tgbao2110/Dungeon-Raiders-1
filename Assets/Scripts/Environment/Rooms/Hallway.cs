using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hallway : MonoBehaviour
{
    [SerializeField] List<Door> doors;
    [SerializeField] TilemapRenderer Background;
    [SerializeField] TilemapRenderer Top;

    public void SetDoorsLocked(bool isLocked)
    {
        foreach (Door door in doors)
        {
            door.SetDoorLocked(isLocked);
        }

        var tilemap = this.GetComponent<TilemapRenderer>();
        tilemap.sortingOrder = isLocked ? 0 : 12;
        if (Background != null && Top != null)
        {
            Background.sortingOrder = isLocked ? -1 : 10;
            Top.sortingOrder = isLocked ? 1 : 13;
        }
    }
}
