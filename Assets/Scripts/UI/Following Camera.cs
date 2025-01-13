using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private bool isPlayerInitialized = false;
 
    public void Initialize(Transform player) 
    {
        this.player = player;
        isPlayerInitialized = true;
    }
    void Update () 
    {
        if (isPlayerInitialized)
        transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
