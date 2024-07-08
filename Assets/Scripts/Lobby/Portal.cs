using System.Collections;
using TMPro;
using UnityEngine;

public class LobbyPortal : MonoBehaviour
{
    private CharacterPicker characterPicker;
    [SerializeField] private TextMeshProUGUI messageText;

    private void Start()
    {
        characterPicker = FindObjectOfType<CharacterPicker>();
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (characterPicker != null && characterPicker.hasPickedCharacter)
            {
                GameManager.Instance.StartGame();
            }
            else
            {
                if (messageText != null)
                {
                    messageText.gameObject.SetActive(true);
                    StartCoroutine(HideMessageAfterDelay(1f));
                }
            }
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }
}
