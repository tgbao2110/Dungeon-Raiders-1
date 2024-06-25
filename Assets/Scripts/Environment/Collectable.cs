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
            //ApplyEffect(other.GetComponentInChildren<PlayerController>());
        }    
    }

    private void ApplyEffect(PlayerController player)
    {
        switch (type)
        {
            case CollectableType.Weapon:
            // Instantiate the weapon at the player's position
            var currentObject = Instantiate(prefab, player.transform.position, Quaternion.identity);
            currentObject.transform.SetParent(player.transform);

            // Set the weapon's local position relative to the player
            Vector3 localPosition = new Vector3(0.2f,-0.15f,0);
            currentObject.transform.localPosition = localPosition;

            player.Equip(currentObject.GetComponent<Weapon>());

            Debug.Log("Picked up Gun");
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
