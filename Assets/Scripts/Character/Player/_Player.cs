using UnityEngine;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Numerics;

public class Player : MonoBehaviour
{
    private CharacterData data;
    private FollowingCamera followingCamera;

    private void Start() {
        Initialize();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public GameObject AddCharacter(CharacterData pData)
    {
        data = pData;
        GameObject sprite = null;
        if (data != null)
        {
            sprite = Instantiate(data.characterPrefab, this.transform.position, UnityEngine.Quaternion.identity);
            sprite.name = data.name;

            if (sprite != null)
            {
                Initialize(sprite);
                var playerController = GetComponentInChildren<PlayerController>();
                playerController.animator = sprite.GetComponentInChildren<Animator>();

                var facingDirection = playerController.GetFacingDirection();
                if (facingDirection.x < 0)
                {
                    sprite.transform.localScale = new UnityEngine.Vector3(-1, 1, 1);
                }

                sprite.transform.SetParent(transform);
                sprite.transform.SetAsFirstSibling();
            }
        }

        return sprite;
    }

    public void StartGame()
    {
        transform.localPosition = new UnityEngine.Vector3(0, 0,0);
        Initialize();
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
        
        followingCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowingCamera>();
        followingCamera.Initialize(transform);
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
