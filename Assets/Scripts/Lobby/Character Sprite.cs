using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Hide()
    {
        spriteRenderer.color = Color.black;
    }
    public void Show()
    {
        spriteRenderer.color = Color.white;
    }
}
