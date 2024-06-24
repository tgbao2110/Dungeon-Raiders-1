using UnityEngine;

public class EventSystem : MonoBehaviour
{
    private void Start()
    {
        // Example of how to subscribe methods to events
        PlayerActions.OnAttack += Attack;
        PlayerActions.OnPickUp += PickUp;
        PlayerActions.OnSwitchWeapon += SwitchWeapon;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        PlayerActions.OnAttack -= Attack;
        PlayerActions.OnPickUp -= PickUp;
        PlayerActions.OnSwitchWeapon -= SwitchWeapon;
    }

    private void Attack()
    {
        // Implement attack logic here
        Debug.Log("Attack action triggered");

        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        Weapon equippedWeapon = player.equippedWeapon;
        equippedWeapon?.AttackAction();
    }

    private void PickUp()
    {
        // Implement pick-up logic here
        Debug.Log("Pick-up action triggered");
    }

    private void SwitchWeapon()
    {
        // Implement switch weapon logic here
        Debug.Log("Switch weapon action triggered");
    }

    // Methods to be called by UI buttons
    public void TriggerAttack()
    {
        PlayerActions.OnAttack?.Invoke();
    }

    public void TriggerPickUp()
    {
        PlayerActions.OnPickUp?.Invoke();
    }

    public void TriggerSwitchWeapon()
    {
        PlayerActions.OnSwitchWeapon?.Invoke();
    }
}