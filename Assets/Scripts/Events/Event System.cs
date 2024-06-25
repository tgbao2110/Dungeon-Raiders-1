using UnityEngine;

public class EventSystem : MonoBehaviour
{
    PlayerController player;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject switchWeaponButton;
    Collectable currentCollectable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();

        // Example of how to subscribe methods to events
        PlayerActions.OnAttack += Attack;
        PlayerActions.OnSwitchWeapon += SwitchWeapon;
        PlayerActions.OnEnterCollectable += ShowSwitchWeaponButton;
        PlayerActions.OnExitCollectable += ShowAttackButton;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        PlayerActions.OnAttack -= Attack;
        PlayerActions.OnSwitchWeapon -= SwitchWeapon;
    }

    private void ShowSwitchWeaponButton()
    {
        attackButton.SetActive(false);
        switchWeaponButton.SetActive(true);
    }

    private void ShowAttackButton()
    {
        switchWeaponButton.SetActive(false);
        attackButton.SetActive(true);
    }

    private void Attack()
    {
        Weapon equippedWeapon = player.equippedWeapon;
        equippedWeapon?.Attack();
    }

    private void SwitchWeapon()
    {
        player.SwitchWeapon(currentCollectable);
    }

    // Methods to be called by UI buttons
    public void TriggerAttack()
    {
        PlayerActions.OnAttack?.Invoke();
    }

    public void TriggerSwitchWeapon()
    {
        PlayerActions.OnSwitchWeapon?.Invoke();
    }

    public void SetCurrentCollectable(Collectable collectable)
    {
         currentCollectable = collectable;
    }
}