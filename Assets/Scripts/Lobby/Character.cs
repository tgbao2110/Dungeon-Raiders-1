using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Armor armor;
    [SerializeField] CharacterPicker characterPicker;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Vector3 offset;
    private SwitchButtonHandler switchButtonHandler;

    private void Awake()
    {
        characterPicker = GetComponentInParent<CharacterPicker>();
        switchButtonHandler = FindObjectOfType<SwitchButtonHandler>();
    }

    private void Update()
    {
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (armor == Armor.Hidden)
            {
                text.text = "???";
            }
            else
            {
                text.text = armor.ToString();
                switchButtonHandler.ShowButton(characterPicker, armor);
            }
            text.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            switchButtonHandler.HideButton();
        }
    }
}
