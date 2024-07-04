using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    GameObject player;
    [SerializeField] float movementSpeed;
    float vInput, hInput;
    public UnityEngine.Vector3 lastFacingDirection {get; private set; }
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] GameObject playerSprite;
    EnemyRoom room;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponentInChildren<Animator>();

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    private void Update() 
    {  
        HandleMovement();  
    }

    void HandleMovement()
    {
        vInput = joystick.Vertical * movementSpeed;
        hInput = joystick.Horizontal * movementSpeed;

        UnityEngine.Vector3 currentDirection = new UnityEngine.Vector3(hInput, vInput, 0).normalized;
        lastFacingDirection = currentDirection;

        if (joystick.Vertical !=0 || joystick.Horizontal !=0)
        {
            animator.SetBool("isWalking",true);
            if (joystick.Horizontal<0)
            {
                player.transform.localScale = new (-1,1,0);
            }
            if (joystick.Horizontal>0)
            {
                player.transform.localScale = new (1,1,0);
            }
        }
        else
        {
            animator.SetBool("isWalking",false);
        }
        
        
        rb.velocity = new UnityEngine.Vector2(hInput, vInput);
    }

    public UnityEngine.Vector3 GetFacingDirection()
    {
        return lastFacingDirection;
    }

    public void SetRoom(EnemyRoom enemyRoom)
    {
        room = enemyRoom;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Paused)
        {
            rb.velocity = UnityEngine.Vector2.zero;
        }
    }
    
}
