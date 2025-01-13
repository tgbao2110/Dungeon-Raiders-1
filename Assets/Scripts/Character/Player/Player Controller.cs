using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPausable
{
    public Joystick joystick;
    public Player player;
    [SerializeField] float movementSpeed;
    private float originalMovementSpeed;
    private float currentMovementSpeed;
    private bool isSlowed = false;
    public bool isMovementEnabled = true;
    float vInput, hInput;
    public Vector3 lastFacingDirection { get; private set; }
    [SerializeField] Rigidbody2D rb;
    public Animator animator;
    EnemyRoom room;

    private void Start()
    {
        RegisterWithPauseManager();
    }

    private void Awake() {
        joystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<Joystick>();
    }

    private void RegisterWithPauseManager()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.RegisterPausable(this);
        }
        else
        {
            StartCoroutine(RetryRegisterWithPauseManager());
        }
    }

    private IEnumerator RetryRegisterWithPauseManager()
    {
        while (PauseManager.Instance == null)
        {
            yield return null; // Wait until the next frame
        }
        PauseManager.Instance.RegisterPausable(this);
    }

    private void OnDestroy()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.UnregisterPausable(this);
        }
    }

    public void Initialize(Rigidbody2D rb, Animator animator)
    {
        RegisterWithPauseManager();
        joystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<Joystick>();

        this.rb = rb;
        this.animator = animator;

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

    private void Update()
    {
        if (isMovementEnabled)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
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

        yield return new WaitForSeconds(duration);

        currentMovementSpeed = originalMovementSpeed;
        isSlowed = false;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
        Debug.Log("Movement enabled");
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
        rb.velocity = Vector2.zero;
        Debug.Log("Movement disabled");
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            DisableMovement();
        }
        else
        {
            EnableMovement();
        }
    }

    public void SetRoom(EnemyRoom enemyRoom) { room = enemyRoom; }
}
