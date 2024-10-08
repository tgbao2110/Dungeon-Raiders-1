    using System;
    using TMPro;
    using UnityEngine;

    public class EventSystem : MonoBehaviour
    {
        PlayerController controller;
        PlayerItemInteraction interaction;
        [SerializeField] GameObject attackButton;
        [SerializeField] GameObject switchWeaponButton;
        [SerializeField] GameObject consumeButton;
        [SerializeField] GameObject ultButton;
        [SerializeField] TextMeshProUGUI ultCountDownText; // Reference to the countdown text
        [SerializeField] GameObject gameOver;
        [SerializeField] GameObject bossIntro; 
        [SerializeField] GameObject textInfo;
        Collectable currentCollectable;
        MeleeAttack meleeAttack;
        Ultimate ultimate;
        CoinUI coinUI;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            controller = player.GetComponentInChildren<PlayerController>();
            interaction = player.GetComponentInChildren<PlayerItemInteraction>();
            meleeAttack = player.GetComponentInChildren<MeleeAttack>();

            Actions.OnAttack += Attack;
            Actions.OnSwitchWeapon += SwitchWeaponPressed;
            Actions.OnUltimate += TriggerUltimate;
            Actions.OnEnterCollectable += ShowSwitchWeaponButton;
            Actions.OnExitCollectable += ShowAttackButton;
            Actions.OnEnterPotion += ShowConsumeButton;
            Actions.OnExitPotion += ShowAttackButton;
            Actions.OnGameOver += ShowGameOverUI;
            Actions.OnEnterBossRoom += ShowBossRoomIntro;
            Actions.OnEnemyRoomCleared += ShowClearUI;
            Actions.OnStartNewRound += ShowLevel;
            Actions.OnCoinCollected += HandleCoinCollected;
        }

        private void OnDestroy()
        {
            Actions.OnAttack -= Attack;
            Actions.OnSwitchWeapon -= SwitchWeaponPressed;
            Actions.OnUltimate -= TriggerUltimate;
            Actions.OnEnterCollectable -= ShowSwitchWeaponButton;
            Actions.OnExitCollectable -= ShowAttackButton;
            Actions.OnEnterPotion -= ShowConsumeButton;
            Actions.OnExitPotion -= ShowAttackButton;
            Actions.OnGameOver -= ShowGameOverUI;
            Actions.OnEnterBossRoom -= ShowBossRoomIntro;
            Actions.OnEnemyRoomCleared -= ShowClearUI;
            Actions.OnStartNewRound -= ShowLevel;
            Actions.OnCoinCollected -= HandleCoinCollected;
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

        public void ShowUltButton()
        {
            ultCountDownText.transform.parent.gameObject.SetActive(false);
            ultButton.SetActive(true);
        }

        public void ShowUltCountDown()
        {
            ultButton.SetActive(false);
            ultCountDownText.transform.parent.gameObject.SetActive(true);
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

        public void DropWeaponPressed()
        {
            interaction.DropWeapon();
        }

        public void TriggerAttack()
        {
            Actions.OnAttack?.Invoke();
        }

        public void TriggerSwitchWeapon()
        {
            Actions.OnSwitchWeapon?.Invoke();
        }

        public void TriggerUltimate()
        {
            ultimate = FindObjectOfType<Ultimate>();
            ultimate.TriggerUltimate();
            ShowUltCountDown();
        }

        public void UpdateUltCooldownText(int remainingTime)
        {
            ultCountDownText.text = remainingTime.ToString();
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

        public void ShowClearUI()
        {
            textInfo.GetComponent<TextMeshProUGUI>().text = "Clear!";
            textInfo.GetComponent<Animator>().SetTrigger("showText");
        }

        public void ShowLevel()
        {
            textInfo.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.GetLevel().ToString() + "-" + GameManager.Instance.GetRound().ToString();
            textInfo.GetComponent<Animator>().SetTrigger("showText");
        }

         private void HandleCoinCollected()
        {
        }
    }
