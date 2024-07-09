using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUltimate : Ultimate
{
    [SerializeField] Animator animator;
    [SerializeField] CircleCollider2D shieldCollider;
    private void Awake()
    {
        shieldCollider.gameObject.SetActive(false);
    }

    protected override void ExecuteUlt()
    {
        animator.SetTrigger("Appear");
        shieldCollider.gameObject.SetActive(true);
        StartCoroutine(ShieldDurationCoroutine(duration));
    }

    private IEnumerator ShieldDurationCoroutine(int duration)
    {
        yield return new WaitForSeconds(duration);

        animator.SetTrigger("Disappear");
        shieldCollider.gameObject.SetActive(false);
    }
}
