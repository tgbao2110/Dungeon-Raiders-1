using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void LoadLevel(int index)
    {
        Debug.Log("Loading level " + index);
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }
}
