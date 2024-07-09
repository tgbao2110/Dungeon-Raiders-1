using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] Button button;

    private void Update() {
        if(GameManager.Instance.GetLastLevel() != 0)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }
    public void Enable()
    {
        button.interactable = true;
    }

    public void Disable()
    {
        button.interactable = false;
    }
}
