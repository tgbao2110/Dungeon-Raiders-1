using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Door : MonoBehaviour
{
    GameObject[] colliders;
    [SerializeField] bool isLocked = false;

    private void Awake()
    {
        isLocked = false;
        colliders = GameObject.FindGameObjectsWithTag("Collider");
    }

    public void SetDoorLocked(bool locked)
    {
        isLocked = locked;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isLocked)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        foreach (var collider in colliders)
        {
            collider.SetActive(false);
        }
    }

    private void CloseDoor()
    {
        foreach (var collider in colliders)
        {
            collider.SetActive(true);
        }
    }
}
