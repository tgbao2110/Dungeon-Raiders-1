using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Player player;
    [SerializeField] float movementSpeed;
    private float originalMovementSpeed;
    private float currentMovementSpeed;
    private bool isSlowed = false;
    float vInput, hInput;
    public Vector3 lastFacingDirection { get; private set; }
    [SerializeField] Rigidbody2D rb;
    public Animator animator;
    EnemyRoom room;

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }

    public void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponentInChildren<Animator>();

        // Ensure everything is initialized properly
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on Player or child objects!");
        }
        if (animator == null)
        {
            Debug.LogError("Animator not found on Player or child objects!");
        }

        originalMovementSpeed = movementSpeed;
        currentMovementSpeed = movementSpeed;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Ensure rb and animator are not null before using them
        if (rb == null || animator == null)
        {
            return;
        }

        vInput = joystick.Vertical * currentMovementSpeed;
        hInput = joystick.Horizontal * currentMovementSpeed;

        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            Vector3 currentDirection = new Vector3(hInput, vInput, 0).normalized;
            lastFacingDirection = currentDirection;

            animator.SetBool("isWalking", true);
            if (joystick.Horizontal < 0)
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (joystick.Horizontal > 0)
            {
                player.transform.localScale = new Vector3(1, 1, 1);
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

    public void ApplySlow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowEffect(slowAmount, duration));
        }
    }

    private IEnumerator SlowEffect(float slowAmount, float duration)
    {
        isSlowed = true;
        currentMovementSpeed = originalMovementSpeed * slowAmount;
        if (player == null) Debug.Log("Player not found");
        FindObjectOfType<Info>().Enable();

        yield return new WaitForSeconds(duration);

        FindObjectOfType<Info>().Disable();
        currentMovementSpeed = originalMovementSpeed;
        isSlowed = false;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Paused)
        {
            rb.velocity = Vector2.zero;
        }
    }
}