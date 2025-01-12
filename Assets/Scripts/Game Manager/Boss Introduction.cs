using System.Collections;
using UnityEngine;

public class BossIntroManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Animator bossIntroAnimator;

    private void Start()
    {
        bossIntroAnimator = panel.GetComponent<Animator>();
    }
    public void StartIntro()
    {
        panel.SetActive(true);
        StartCoroutine(PlayingIntro());
    }

    IEnumerator PlayingIntro()
    {
        yield return new WaitForSeconds(1);
        bossIntroAnimator.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        EndIntro();
    }

    void EndIntro()
    {
        panel.SetActive(false);
    }
}
