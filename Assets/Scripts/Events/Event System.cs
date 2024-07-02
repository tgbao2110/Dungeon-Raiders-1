using UnityEngine;

public class EventSystem : MonoBehaviour
{
    PlayerController player;
    PlayerItemInteraction interaction;
    Health health;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject switchWeaponButton;
    [SerializeField] GameObject gameOver;
    Collectable currentCollectable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        interaction = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerItemInteraction>();

        // Example of how to subscribe methods to events
        PlayerActions.OnAttack += Attack;
        PlayerActions.OnSwitchWeapon += SwitchWeaponPressed;
        PlayerActions.OnEnterCollectable += ShowSwitchWeaponButton;
        PlayerActions.OnExitCollectable += ShowAttackButton;
        PlayerActions.OnGameOver += ShowGameOverUI;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        PlayerActions.OnAttack -= Attack;
        PlayerActions.OnSwitchWeapon -= SwitchWeaponPressed;
    }

    private void ShowGameOverUI()
    {
        gameOver.SetActive(true);
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
        Weapon equippedWeapon = interaction.equippedWeapon;
        equippedWeapon?.Attack();
    }

    private void SwitchWeaponPressed()
    {
        interaction.SwitchWeaponPressed(currentCollectable);
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

    public void TriggerGameOver()
    {
        PlayerActions.OnGameOver?.Invoke();
    }

    public void SetCurrentCollectable(Collectable collectable)
    {
        currentCollectable = collectable;
    }
}