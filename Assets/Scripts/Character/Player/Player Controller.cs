using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    GameObject player;
    [SerializeField] float movementSpeed;
    float vInput, hInput;
    public Vector3 lastFacingDirection { get; private set; }
    private Vector3 lastNonZeroDirection = Vector3.right; // Default facing direction (right)
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerSprite;
    EnemyRoom room;

    private void Awake()
    {
        Initialize(playerSprite);
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
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

        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            animator.SetBool("isWalking", true);
            lastFacingDirection = currentDirection;

            if (joystick.Horizontal != 0)
            {
                lastNonZeroDirection = new Vector3(Mathf.Sign(joystick.Horizontal), 0, 0);
                player.transform.localScale = new Vector3(Mathf.Sign(joystick.Horizontal), 1, 1);
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
        if (lastFacingDirection == Vector3.zero)
        {
            return lastNonZeroDirection;
        }
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
