using UnityEngine;
using UnityEngine.UI;

public class SwitchButtonHandler : MonoBehaviour
{
    [SerializeField] private Button switchButton;
    [SerializeField] private Button attackButton;
    private Armor currentArmor;
    private CharacterPicker characterPicker;

    private void Awake()
    {
        switchButton.gameObject.SetActive(false);
        switchButton.onClick.AddListener(OnSwitchButtonClicked);
    }

    public void ShowButton(CharacterPicker picker, Armor armor)
    {
        characterPicker = picker;
        currentArmor = armor;
        switchButton.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(false);
    }

    public void HideButton()
    {
        switchButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(true);
    }

    private void OnSwitchButtonClicked()
    {
        characterPicker.Equip(currentArmor);
        HideButton();
    }
}
