using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public CollectableType type;
    [SerializeField] GameObject prefab;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            ApplyEffect(other.GetComponentInChildren<PlayerController>());
        }    
    }

    private void ApplyEffect(PlayerController player)
    {
        switch(type)
        {
            case CollectableType.Weapon:
                var currentObject = Instantiate(prefab, player.transform.position,Quaternion.identity);
                currentObject.transform.SetParent(player.transform);
                currentObject.transform.position += new Vector3((float)0.4,(float)-0.15,0);
                //player.EquipWeapon();

                Debug.Log("picked up Gun");
                break;
        }

        Destroy(this.gameObject);
    }

    public enum CollectableType
    {
        Weapon,
        Comsumable
    }
}
