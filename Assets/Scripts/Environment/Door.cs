using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Door : MonoBehaviour
{
    GameObject[] colliders; 
    [SerializeField] DungeonGenerator dg;

    private void Awake() {
        
        colliders = GameObject.FindGameObjectsWithTag("Collider");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" )
        {
            foreach (var collider in colliders)
            collider.SetActive(false);
        }
        else{}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
        {
            foreach (var collider in colliders)
            collider.SetActive(true);
        }
    }
}
