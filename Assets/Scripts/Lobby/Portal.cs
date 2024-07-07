using System.Collections;
using TMPro;
using UnityEngine;

public class LobbyPortal : MonoBehaviour
{
    private CharacterPicker characterPicker;
    [SerializeField] private TextMeshProUGUI messageText; // Reference to your UI Text element

    private void Start()
    {
        characterPicker = FindObjectOfType<CharacterPicker>();
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // Ensure the message is initially hidden
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
                Debug.Log("Player has not picked a character yet.");
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
