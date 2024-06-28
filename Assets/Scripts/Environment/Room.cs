using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    protected Hallway fromHallway, toHallway;
    public abstract void InitializeRoom();

    
    public void SetFromHallway(Hallway hallway)
    {
        fromHallway = hallway;;
    }

    public void SetToHallway(Hallway hallway)
    {
        toHallway = hallway;
    }
}
