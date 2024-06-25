using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    public Weapon equippedWeapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            PlayerActions.OnEnterCollectable?.Invoke();

            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                eventSystem.SetCurrentCollectable(other.GetComponent<Collectable>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            PlayerActions.OnExitCollectable?.Invoke();
            // Clear the current collectable in EventSystem
            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                eventSystem.SetCurrentCollectable(null);
            }
        }
    }

    public void Equip(Weapon weapon)
    {
        weapon.playerController = transform.parent.GetComponentInChildren<PlayerController>();
        equippedWeapon = weapon;
    }

    public void SwitchWeaponPressed(Collectable item)
    {
        if (item != null)
        {
            Debug.Log("Switching weapon with collectable: " + item.name);
            PickUpWeapon(item);
            Destroy(item.gameObject);
        }
        else
        {
            Debug.Log("No collectable found");
        }
    }

    void DropWeapon()
    {
        if (equippedWeapon != null)
        {
            // Create a collectable item with the weapon prefab at the player's position
            GameObject droppedCollectable = new GameObject(equippedWeapon.WeaponData.name + "_clt");
            droppedCollectable.transform.position = transform.position+ new Vector3(1,0,0);
            Collectable col = droppedCollectable.AddComponent<Collectable>();
            col.Initialize(equippedWeapon.WeaponData, Collectable.CollectableType.Weapon);


            Destroy(equippedWeapon.gameObject); // Remove the equipped weapon from the player
            equippedWeapon = null;
        }
    }

    void PickUpWeapon(Collectable item)
    {
        if (item.type == Collectable.CollectableType.Weapon)
        {
            Debug.Log("Picked Up " + item.name);
            // Drop the current weapon if one is equipped
            DropWeapon();

            // Instantiate the weapon at the player's position
            var currentObject = Instantiate(item.itemData.prefab, this.transform.position, Quaternion.identity);
            currentObject.transform.SetParent(this.transform);

            // Set the weapon's local position relative to the player
            Vector3 localPosition = new Vector3(0.3f, -0.25f, 0);
            currentObject.transform.localPosition = localPosition;

            currentObject.GetComponent<Weapon>().Initialize();
            Equip(currentObject.GetComponent<Weapon>());
        }
    }
}
