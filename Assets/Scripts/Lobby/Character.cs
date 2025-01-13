using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] bool isLocked = true;
    public CharacterSprite sprite;
    public Armor armor;
    public CharacterPicker characterPicker;
    public TextMeshProUGUI text;
    public UnlockText unlockText;
    public Vector3 offset;
    public LobbyButton lobbyButton;
    private CharacterState currentState;

    private void Start()
    {
        characterPicker = GetComponentInParent<CharacterPicker>();
        lobbyButton = FindObjectOfType<LobbyButton>();
        sprite = GetComponentInChildren<CharacterSprite>();
        SetState(isLocked ? new LockedState() : new UnlockedState());
        currentState.StateEnter(this);
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.CollisionEnter(this);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.CollisionExit(this);
        }
    }

    public void SetState(CharacterState newState)
    {
        currentState = newState;
        currentState.StateEnter(this);
    }

    public void ConfirmPurchase()
    {
        unlockText.OnPurchaseClicked(this);
    }
}
