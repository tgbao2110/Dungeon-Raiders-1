using UnityEngine;

public class EventSystem : MonoBehaviour
{
    PlayerController controller;
    PlayerItemInteraction interaction;
    Health health;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject switchWeaponButton;
    [SerializeField] GameObject consumeButton;   
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject bossIntro; 
    Collectable currentCollectable;
    MeleeAttack meleeAttack;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponentInChildren<PlayerController>();
        interaction = player.GetComponentInChildren<PlayerItemInteraction>();
        meleeAttack = player.GetComponentInChildren<MeleeAttack>();

        Actions.OnAttack += Attack;
        Actions.OnSwitchWeapon += SwitchWeaponPressed;
        Actions.OnEnterCollectable += ShowSwitchWeaponButton;
        Actions.OnExitCollectable += ShowAttackButton;
        Actions.OnEnterPotion += ShowConsumeButton;
        Actions.OnExitPotion += ShowAttackButton;
        Actions.OnGameOver += ShowGameOverUI;
        Actions.OnEnterBossRoom += ShowBossRoomIntro;
    }

    private void OnDestroy()
    {
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
        consumeButton.SetActive(false);
    }

    private void ShowConsumeButton()
    {
        attackButton.SetActive(false);
        switchWeaponButton.SetActive(false);
        consumeButton.SetActive(true);
    }

    private void ShowAttackButton()
    {
        switchWeaponButton.SetActive(false);
        attackButton.SetActive(true);
        consumeButton.SetActive(false);
    }

    private void Attack()
    {
        Weapon equippedWeapon = interaction.equippedWeapon;
        if (equippedWeapon != null)
        {
            equippedWeapon.Attack();
        }
        else
        {
            meleeAttack.PerformAttack();
        }
    }

    private void SwitchWeaponPressed()
    {
        interaction.SwitchWeaponPressed(currentCollectable);
    }

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

    public void TriggerConsume()
    {
        interaction.Consume();
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