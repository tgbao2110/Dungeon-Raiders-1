using UnityEngine;
using BarthaSzabolcs.Tutorial_SpriteFlash;

public class Player : MonoBehaviour
{
    private CharacterData data;

    private void Start() {
        Initialize();
    }

    public GameObject AddCharacter(CharacterData pData)
    {
        data = pData;
        GameObject sprite = null;
        if (data != null)
        {
            sprite = Instantiate(data.characterPrefab, this.transform.position, Quaternion.identity);
            sprite.name = data.name;

            if (sprite != null)
            {
                Initialize(sprite);
                var playerController = GetComponentInChildren<PlayerController>();
                playerController.animator = sprite.GetComponentInChildren<Animator>();

                var facingDirection = playerController.GetFacingDirection();
                if (facingDirection.x < 0)
                {
                    sprite.transform.localScale = new Vector3(-1, 1, 1);
                }

                sprite.transform.SetParent(transform);
                sprite.transform.SetAsFirstSibling();
            }
        }

        return sprite;
    }

    public void Initialize()
    {
        var playerController = GetComponentInChildren<PlayerController>();
        if (playerController != null)
        {
            var rb = GetComponentInChildren<Rigidbody2D>();
            var animator = GetComponentInChildren<Animator>();
            playerController.player = this;
            playerController.Initialize(rb, animator);
        }
    }

    public void Initialize(GameObject sprite)
    {
        var health = GetComponentInChildren<Health>();
        if (health != null)
        {
            health.Initialize(data, sprite.GetComponent<SimpleFlash>());
        }
        
        Initialize();
        
    }
}
