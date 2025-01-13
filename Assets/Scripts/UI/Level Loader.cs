using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void LoadLevel(string name)
    {
        Debug.Log("Loading scene " + name);
        StartCoroutine(LoadScene(name));
    }

    IEnumerator LoadScene(string name)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        // SceneManager.LoadScene(index, LoadSceneMode.Single);
        NetworkManager.singleton.ServerChangeScene(name);
    }
}
