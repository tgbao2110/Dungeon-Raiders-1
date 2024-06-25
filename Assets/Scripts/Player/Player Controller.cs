using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    GameObject player;
    [SerializeField] float movementSpeed;
    float vInput, hInput;
    UnityEngine.Vector3 lastFacingDirection;
    Rigidbody2D rb;
    Animator animator;
    public Weapon equippedWeapon;
    [SerializeField] GameObject playerSprite;

    // Start is called before the first frame update
    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponentInChildren<Animator>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            PlayerActions.OnEnterCollectable?.Invoke();

            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                eventSystem.SetCurrentCollectable(other.GetComponent<Collectable>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            PlayerActions.OnExitCollectable?.Invoke();
            // Clear the current collectable in EventSystem
            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                eventSystem.SetCurrentCollectable(null);
            }
        }
    }

    public void Equip(Weapon weapon)
    {
        equippedWeapon = weapon;
    }

    public void SwitchWeapon(Collectable collectable)
    {
        if (collectable != null)
        {
            // Implement your logic to switch weapon with the collectable object
            Debug.Log("Switching weapon with collectable: " + collectable.name);
            //Destroy(equippedWeapon.gameObject); // Destroy the current weapon
            //Equip(collectable.GetComponent<Weapon>()); // Equip the collectable weapon
            //Destroy(collectable.gameObject); // Destroy the collectable object
        }
        else
        {
            Debug.Log("No coll found");
        }
    }

    
}
