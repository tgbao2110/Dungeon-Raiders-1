using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RewardChest : MonoBehaviour
{
    GameObject itemToDrop;
    bool isEmpty = false;
    Animator animator;

    public void InitializeChest(GameObject itemToDrop)
    {
        this.itemToDrop = itemToDrop;
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isEmpty)
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        animator.SetTrigger("open");
        Instantiate(itemToDrop, transform.position + new Vector3(0,-1,0), Quaternion.identity);
        isEmpty = true;
        itemToDrop = null;
    }
}
