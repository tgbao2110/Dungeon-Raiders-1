using System.Collections;
using UnityEngine;
using TMPro;

public abstract class Ultimate : MonoBehaviour
{
    [SerializeField] protected int duration;
    [SerializeField] protected int coolDownDuration;
    protected bool isOnCooldown = false;
    private EventSystem eventSystem;

    protected abstract void ExecuteUlt();

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void TriggerUltimate()
    {
        if (!isOnCooldown)
        {
            ExecuteUlt();
            StartCooldown();
        }
    }

    protected void StartCooldown()
    {
        eventSystem.ShowUltCountDown();
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        int remainingTime = coolDownDuration;
        while (remainingTime > 0)
        {
            eventSystem.UpdateUltCooldownText(remainingTime);
            yield return new WaitForSeconds(1);
            remainingTime--;
        }
        eventSystem.ShowUltButton();
    }
}
