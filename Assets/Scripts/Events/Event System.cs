using UnityEngine;

public class EventSystem : MonoBehaviour
{
    PlayerController player;
    PlayerItemInteraction interaction;
    Health health;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject switchWeaponButton;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject bossIntro;
    Collectable currentCollectable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        interaction = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerItemInteraction>();

        // Example of how to subscribe methods to events
        Actions.OnAttack += Attack;
        Actions.OnSwitchWeapon += SwitchWeaponPressed;
        Actions.OnEnterCollectable += ShowSwitchWeaponButton;
        Actions.OnExitCollectable += ShowAttackButton;
        Actions.OnGameOver += ShowGameOverUI;
        Actions.OnEnterBossRoom += ShowBossRoomIntro;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        Actions.OnAttack -= Attack;
        Actions.OnSwitchWeapon -= SwitchWeaponPressed;
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
        Actions.OnAttack?.Invoke();
    }

    public void TriggerSwitchWeapon()
    {
        Actions.OnSwitchWeapon?.Invoke();
    }

    public void TriggerGameOver()
    {
        Actions.OnGameOver?.Invoke();
    }

    public void SetCurrentCollectable(Collectable collectable)
    {
        currentCollectable = collectable;
    }

    public void ShowBossRoomIntro()
    {
        bossIntro.GetComponent<BossIntroManager>().StartIntro();
    }
}