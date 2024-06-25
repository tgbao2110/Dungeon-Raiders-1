using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemData itemData;
    public CollectableType type;

    public void Initialize(ItemData itemData, CollectableType type)
    {
        this.gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.sprite;
        spriteRenderer.sortingLayerName = "Characters"; 
        this.gameObject.AddComponent<BoxCollider2D>();
        this.GetComponent<BoxCollider2D>().isTrigger = true;
        this.tag = "Collectable";
        this.type = type;
        this.itemData = itemData;
    }

    public enum CollectableType
    {
        Weapon,
        Comsumable
    }
}
