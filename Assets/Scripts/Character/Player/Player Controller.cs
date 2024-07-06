using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    GameObject player;
    [SerializeField] float movementSpeed;
    float vInput, hInput;
    public Vector3 lastFacingDirection { get; private set; }
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerSprite;
    EnemyRoom room;

    private void Awake()
    {
        Initialize(playerSprite);
    }

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }

    public void Initialize(GameObject playerSprite)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        animator = playerSprite.GetComponentInChildren<Animator>();
    }

    private void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    public void SetSprite(GameObject sprite)
    {
        playerSprite = sprite;
    }

    void HandleMovement()
    {
        vInput = joystick.Vertical * movementSpeed;
        hInput = joystick.Horizontal * movementSpeed;

        Vector3 currentDirection = new Vector3(hInput, vInput, 0).normalized;
        lastFacingDirection = currentDirection;

        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            animator.SetBool("isWalking", true);
            if (joystick.Horizontal < 0)
            {
                player.transform.localScale = new Vector3(-1, 1, 0);
            }
            if (joystick.Horizontal > 0)
            {
                player.transform.localScale = new Vector3(1, 1, 0);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        rb.velocity = new Vector2(hInput, vInput);
    }

    public Vector3 GetFacingDirection()
    {
        return lastFacingDirection;
    }

    public void SetRoom(EnemyRoom enemyRoom)
    {
        room = enemyRoom;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        Initialize();
        if (newGameState == GameState.Paused)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
