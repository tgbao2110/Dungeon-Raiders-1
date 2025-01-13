using TMPro;
using UnityEngine;

public class UnlockText : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private UnlockTextState currentState;

    private void Start()
    {
        SetState(new Empty());
    }

    public void SetState(UnlockTextState newState)
    {
        currentState = newState;
        currentState.StateEnter(this);
    }

    public void OnPurchaseClicked(Character character)
    {
        currentState.PurchaseClicked(this, character);
    }
}
