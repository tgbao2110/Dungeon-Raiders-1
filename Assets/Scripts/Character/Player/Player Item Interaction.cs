using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    public Weapon equippedWeapon;
    private Potion currentPotion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            Actions.OnEnterCollectable?.Invoke();
            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                eventSystem.SetCurrentCollectable(other.GetComponent<Collectable>());
            }
        }
        else if (other.CompareTag("Potion"))
        {
            Actions.OnEnterPotion?.Invoke();
            currentPotion = other.GetComponent<Potion>();
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoins(1);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            Actions.OnExitCollectable?.Invoke();
            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                eventSystem.SetCurrentCollectable(null);
            }
        }
        else if (other.CompareTag("Potion"))
        {
            Actions.OnExitPotion?.Invoke();
            currentPotion = null;
        }
    }

    public void Equip(Weapon weapon)
    {
        weapon.playerController = transform.parent.GetComponentInChildren<PlayerController>();
        equippedWeapon = weapon;
        FindObjectOfType<WeaponPanel>().SetWeapon(weapon.weaponData);
    }

    public void SwitchWeaponPressed(Collectable item)
    {
        if (item != null)
        {
            PickUpWeapon(item);
            Destroy(item.gameObject);
        }
        else
        {
            Debug.Log("No collectable found");
        }
    }

    public void Consume()
    {
        if (currentPotion != null)
        {
            currentPotion.Restore(this.transform.parent.gameObject);
            Destroy(currentPotion.gameObject);
            currentPotion = null;
        }
        else
        {
            Debug.Log("No potion found");
        }
    }

    public void DropWeapon()
    {
        if (equippedWeapon != null)
        {
            GameObject droppedCollectable = new GameObject(equippedWeapon.weaponData.name + "_clt");
            droppedCollectable.transform.position = transform.position + new Vector3(1, 0, 0);
            Collectable col = droppedCollectable.AddComponent<Collectable>();
            col.Initialize(equippedWeapon.weaponData);

            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;
        }
    }

    private void PickUpWeapon(Collectable item)
    {
        DropWeapon();

        var currentObject = Instantiate(item.weaponData.prefab, this.transform.position, Quaternion.identity);
        currentObject.transform.SetParent(this.transform);

        Vector3 localPosition = new Vector3(0.3f, -0.25f, 0);
        currentObject.transform.localPosition = localPosition;

        currentObject.GetComponent<Weapon>().Initialize(item.weaponData);
        Equip(currentObject.GetComponent<Weapon>());
    }

        public void SaveWeaponState()
    {
        if (equippedWeapon != null)
        {
            GameManager.Instance.savedWeaponData = equippedWeapon.weaponData;
        }
    }

    public void LoadWeaponState()
    {
        if (GameManager.Instance.savedWeaponData != null)
        {
            var weaponPrefab = GameManager.Instance.savedWeaponData.prefab;
            var weaponObject = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
            weaponObject.transform.localPosition = new Vector3(0.3f, -0.25f, 0);
            Weapon weaponComponent = weaponObject.GetComponent<Weapon>();
            weaponComponent.Initialize(GameManager.Instance.savedWeaponData);
            Equip(weaponComponent);
        }
    }
}
