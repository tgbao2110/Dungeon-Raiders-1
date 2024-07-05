using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public WeaponData weaponData;

    public void Initialize(WeaponData itemData)
    {
        this.gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.sprite;
        spriteRenderer.sortingLayerName = "Characters"; 
        this.gameObject.AddComponent<BoxCollider2D>();
        this.GetComponent<BoxCollider2D>().isTrigger = true;
        this.tag = "Collectable";
        this.weaponData = itemData;
    }
}
